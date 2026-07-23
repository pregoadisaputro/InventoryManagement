using FluentValidation;
using InventoryManagement.Api.Data;
using InventoryManagement.Api.Data.Entity;
using InventoryManagement.Api.Data.Enum;
using InventoryManagement.Api.Features.Transactions.Constant;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Transactions.Endpoints.CreateTransaction;

public static class CreateTransaction
{
    public static void MapCreateTransaction(this IEndpointRouteBuilder group)
    {
        group
            .MapPost(
                "/",
                async (
                    int productId,
                    CreateTransactionRequest request,
                    IValidator<CreateTransactionRequest> validator,
                    AppDbContext db,
                    ILogger<Program> logger,
                    CancellationToken cancellationToken
                ) =>
                {
                    var validationResult = await validator.ValidateAsync(
                        request,
                        cancellationToken
                    );

                    if (!validationResult.IsValid)
                    {
                        return Results.ValidationProblem(validationResult.ToDictionary());
                    }

                    var product = await db.Products.FirstOrDefaultAsync(
                        p => p.Id == productId,
                        cancellationToken
                    );

                    if (product is null)
                    {
                        return Results.NotFound($"Product with ID {productId} does not exist.");
                    }

                    if (request.Quantity <= 0)
                    {
                        return Results.BadRequest("Quantity must be greater than zero.");
                    }

                    var previousStock = product.Stock;
                    var newStock = previousStock;

                    switch (request.Type)
                    {
                        case TransactionType.StockIn:
                            newStock += request.Quantity;
                            break;

                        case TransactionType.StockOut:
                            if (previousStock < request.Quantity)
                            {
                                return Results.BadRequest(
                                    $"Cannot remove {request.Quantity} units. Current stock is {previousStock}"
                                );
                            }

                            newStock -= request.Quantity;
                            break;

                        case TransactionType.Adjustment:
                            newStock += request.Quantity;

                            if (newStock < 0)
                            {
                                return Results.BadRequest("Stock cannot be negative.");
                            }

                            break;

                        default:
                            return Results.BadRequest("Invalid transaction type.");
                    }

                    product.Stock = newStock;
                    product.UpdatedAt = DateTimeOffset.UtcNow;

                    var transaction = new InventoryTransaction
                    {
                        ProductId = productId,
                        Notes = request.Notes,
                        Quantity = request.Quantity,
                        Type = request.Type,
                        PreviousStock = previousStock,
                        NewStock = newStock,
                    };

                    db.Transactions.Add(transaction);

                    try
                    {
                        await db.SaveChangesAsync(cancellationToken);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return Results.Conflict(
                            "This product was updated by another request. Please retry with the latest data."
                        );
                    }

                    logger.LogInformation(
                        "Transaction created for {ProductID} with transaction ID {TransactionId}",
                        transaction.ProductId,
                        transaction.Id
                    );

                    return Results.CreatedAtRoute(
                        TransactionsEndpointsNames.GetTransaction,
                        new { productId, id = transaction.Id },
                        new CreateTransactionResponse(
                            transaction.Id,
                            transaction.Notes,
                            transaction.Quantity,
                            transaction.Type,
                            transaction.PreviousStock,
                            transaction.NewStock,
                            transaction.CreatedAt
                        )
                    );
                }
            )
            .Produces<CreateTransactionResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict);
    }
}
