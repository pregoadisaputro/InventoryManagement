namespace InventoryManagement.Api.Features.Products.Endpoints.CreateProduct;

public record CreateProductRequest(
    string Name,
    string Sku,
    string Description,
    decimal Price,
    int Stock,
    int MinimumStock,
    int CategoryId,
    int? SuppplierId
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
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt
);
