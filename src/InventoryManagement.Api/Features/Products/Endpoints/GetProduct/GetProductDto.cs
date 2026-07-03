namespace InventoryManagement.Api.Features.Products.Endpoints.GetProduct;

public record GetProductByIdRequest(int Id);

public record GetProductByIdResponse(
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
