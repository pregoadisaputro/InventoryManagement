namespace InventoryManagement.Api.Features.Products.CreateProduct;

public record CreateProductRequest(
    string Name,
    string Sku,
    string Description,
    decimal Price,
    int Stock,
    int MinimumStock,
    int CategoryId
);

public record CreateProductResponse(
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
