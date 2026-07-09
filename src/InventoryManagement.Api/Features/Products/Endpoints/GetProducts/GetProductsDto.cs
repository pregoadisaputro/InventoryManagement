namespace InventoryManagement.Api.Features.Products.Endpoints.GetProducts;

public record GetProductsRequest(int PageNumber, int PageSize, string? Name = null);

public record GetProductsPage(
    int PageNumber,
    int PageSize,
    int TotalItems,
    int TotalPages,
    IEnumerable<GetProductsResponse> Data
);

public record GetProductsResponse(
    int Id,
    string Name,
    string Sku,
    string? Description,
    decimal Price,
    int Stock,
    int MinimumStock,
    int CategoryId,
    string CategoryName,
    int? SupplierId,
    string? SupplierName,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt
);
