namespace InventoryManagement.Api.Features.Products.Endpoints.GetProduct;

public record GetProductResponse(
    int Id,
    string Name,
    string Sku,
    string Description,
    decimal Price,
    int Stock,
    int MinimumStock,
    int CategoryId,
    string CategoryName,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt
);
