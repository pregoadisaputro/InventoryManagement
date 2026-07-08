using InventoryManagement.Api.Data.Enum;

namespace InventoryManagement.Api.Features.Transactions.Endpoints.GetTransaction;

public record GetTransactionResponse(
    int Id,
    int ProductId,
    string ProductName,
    string? Notes,
    int Quantity,
    TransactionType Type,
    int PreviousStock,
    int NewStock,
    DateTimeOffset CreatedAt
);
