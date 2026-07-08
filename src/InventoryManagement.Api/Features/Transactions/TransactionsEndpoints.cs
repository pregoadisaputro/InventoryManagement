using InventoryManagement.Api.Features.Transactions.Endpoints.CreateTransaction;

namespace InventoryManagement.Api.Features.Transactions;

public static class TransactionsEndpoints
{
    public static void MapTransactions(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products/{productId:int}/transactions");

        group.MapCreateTransaction();
    }
}
