using InventoryManagement.Api.Data;
using InventoryManagement.Api.Features.Transactions.Constant;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Transactions.Endpoints.GetTransaction;

public static class GetTransaction
{
    public static void MapGetTransaction(this IEndpointRouteBuilder group)
    {
        group
            .MapGet(
                "/{id:int}",
                async (
                    int productId,
                    int id,
                    AppDbContext db,
                    CancellationToken cancellationToken
                ) =>
                {
                    var response = await db
                        .Transactions.AsNoTracking()
                        .Where(t => t.Id == id && t.ProductId == productId)
                        .Select(t => new GetTransactionResponse(
                            t.Id,
                            t.ProductId,
                            t.Product.Name,
                            t.Notes,
                            t.Quantity,
                            t.Type,
                            t.PreviousStock,
                            t.NewStock,
                            t.CreatedAt
                        ))
                        .FirstOrDefaultAsync(cancellationToken);

                    if (response is null)
                    {
                        return Results.NotFound($"Transaction with ID {id} was not found.");
                    }

                    return Results.Ok(response);
                }
            )
            .WithName(TransactionsEndpointsNames.GetTransaction)
            .Produces<GetTransactionResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }
}
