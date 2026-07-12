using InventoryManagement.Api.Data;
using InventoryManagement.Api.Features.Transactions.Constant;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Transactions.Endpoints.GetTransactions;

public static class GetTransactions
{
    public static void MapGetTransactions(this IEndpointRouteBuilder group)
    {
        group
            .MapGet(
                "/",
                async (
                    [AsParameters] GetTransactionsRequest request,
                    AppDbContext db,
                    CancellationToken cancellationToken
                ) =>
                {
                    var pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
                    var pageSize = request.PageSize switch
                    {
                        < 1 => 5,
                        > 100 => 100,
                        _ => request.PageSize,
                    };

                    var skip = (pageNumber - 1) * pageSize;

                    var query = db.Transactions.AsNoTracking().AsQueryable();

                    if (request.ProductId is not null)
                    {
                        query = query.Where(t => t.ProductId == request.ProductId);
                    }

                    if (request.Type is not null)
                    {
                        query = query.Where(t => t.Type == request.Type);
                    }

                    var totalTransactions = await query.CountAsync(cancellationToken);
                    var totalPages = (int)Math.Ceiling((double)totalTransactions / pageSize);

                    var response = await query
                        .OrderBy(t => t.Id)
                        .Skip(skip)
                        .Take(pageSize)
                        .Select(t => new GetTransactionsResponse(
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
                        .ToListAsync(cancellationToken);

                    return Results.Ok(
                        new GetTransactionsPage(
                            pageNumber,
                            pageSize,
                            totalTransactions,
                            totalPages,
                            response
                        )
                    );
                }
            )
            .WithName(TransactionsEndpointsNames.GetTransactions)
            .Produces<GetTransactionsPage>(StatusCodes.Status200OK);
    }
}
