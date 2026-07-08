using InventoryManagement.Api.Data.Enum;

namespace InventoryManagement.Api.Data.Entity;

public class InventoryTransaction
{
    public int Id { get; set; }
    public string? Notes { get; set; }
    public int Quantity { get; set; }
    public TransactionType Type { get; set; }
    public int PreviousStock { get; set; }
    public int NewStock { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
