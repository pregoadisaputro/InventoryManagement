namespace InventoryManagement.Api.Features.Products.Endpoints.GetProducts;

public record GetProductsRequest(
    int PageNumber = 1,
    int PageSize = 5,
    string? Name = null,
    int? CategoryId = null,
    int? SupplierId = null,
    bool? LowStock = null,
    bool? OutOfStock = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null
);

public record GetProductsPage(
    int PageNumber,
    int PageSize,
    int TotalItems,
    int TotalPages,
    IReadOnlyList<GetProductsResponse> Data
);

public record GetProductsResponse(
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
