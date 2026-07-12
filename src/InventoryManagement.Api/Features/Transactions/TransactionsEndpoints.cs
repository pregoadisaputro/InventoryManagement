using InventoryManagement.Api.Features.Transactions.Endpoints.CreateTransaction;
using InventoryManagement.Api.Features.Transactions.Endpoints.GetTransaction;
using InventoryManagement.Api.Features.Transactions.Endpoints.GetTransactions;

namespace InventoryManagement.Api.Features.Transactions;

public static class TransactionsEndpoints
{
    public static void MapTransactions(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products/{productId:int}/transactions").RequireAuthorization();

        group.MapCreateTransaction();

        group.MapGetTransaction();
        group.MapGetTransactions();
    }
}
