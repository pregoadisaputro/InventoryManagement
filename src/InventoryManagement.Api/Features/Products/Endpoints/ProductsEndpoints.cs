using InventoryManagement.Api.Features.Products.Endpoints.CreateProduct;
using InventoryManagement.Api.Features.Products.Endpoints.GetProduct;

namespace InventoryManagement.Api.Features.Products.Endpoints;

public static class ProductsEndpoints
{
    public static void MapProducts(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products");

        group.MapCreateProduct();
        group.MapGetProductById();
    }
}
