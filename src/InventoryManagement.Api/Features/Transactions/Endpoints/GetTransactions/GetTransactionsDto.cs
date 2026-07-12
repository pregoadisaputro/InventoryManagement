using InventoryManagement.Api.Data.Enum;

namespace InventoryManagement.Api.Features.Transactions.Endpoints.GetTransactions;

public record GetTransactionsRequest(
    int PageNumber = 1,
    int PageSize = 5,
    int? ProductId = null,
    TransactionType? Type = null
);

public record GetTransactionsPage(
    int PageNumber,
    int PageSize,
    int TotalTransactions,
    int TotalPages,
    IReadOnlyList<GetTransactionsResponse> Data
);

public record GetTransactionsResponse(
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
