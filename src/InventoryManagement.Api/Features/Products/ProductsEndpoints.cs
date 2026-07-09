using InventoryManagement.Api.Features.Products.Endpoints.CreateProduct;
using InventoryManagement.Api.Features.Products.Endpoints.GetProduct;
using InventoryManagement.Api.Features.Products.Endpoints.GetProducts;

namespace InventoryManagement.Api.Features.Products;

public static class ProductsEndpoints
{
    public static void MapProducts(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products");

        group.MapCreateProduct();
        group.MapGetProduct();
        group.MapGetProducts();
    }
}
