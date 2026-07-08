using InventoryManagement.Api.Data.Enum;

namespace InventoryManagement.Api.Features.Transactions.Endpoints.CreateTransaction;

public record CreateTransactionRequest(string? Notes, int Quantity, TransactionType Type);

public record CreateTransactionResponse(
    int Id,
    string? Notes,
    int Quantity,
    TransactionType Type,
    int PreviousStock,
    int NewStock,
    DateTimeOffset CreatedAt
);
