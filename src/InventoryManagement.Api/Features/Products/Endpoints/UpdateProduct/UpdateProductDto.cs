namespace InventoryManagement.Api.Features.Products.Endpoints.UpdateProduct;

public record UpdateProductRequest(
    string Name,
    string Sku,
    string? Description,
    decimal Price,
    int MinimumStock,
    string? ImgUrl,
    int CategoryId,
    int? SupplierId
);
