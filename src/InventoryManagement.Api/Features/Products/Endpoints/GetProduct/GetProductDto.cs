namespace InventoryManagement.Api.Features.Products.Endpoints.GetProduct;

public record GetProductResponse(
    int Id,
    string Name,
    string Sku,
    string? Description,
    decimal Price,
    int Stock,
    int MinimumStock,
    string? ImgUrl,
    int CategoryId,
    string CategoryName,
    int? SupplierId,
    string? SupplierName,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt
);
