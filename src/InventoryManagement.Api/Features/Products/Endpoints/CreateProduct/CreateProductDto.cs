namespace InventoryManagement.Api.Features.Products.Endpoints.CreateProduct;

public record CreateProductRequest(
    string Name,
    string Sku,
    string? Description,
    decimal Price,
    int Stock,
    int MinimumStock,
    string? ImgUrl,
    int CategoryId,
    int? SupplierId
);

public record CreateProductResponse(
    int Id,
    string Name,
    string Sku,
    string? Description,
    decimal Price,
    int Stock,
    int MinimumStock,
    string? ImgUrl,
    int CategoryId,
    int? SupplierId,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt
);
