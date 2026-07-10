using InventoryManagement.Api.Features.Products.Endpoints.CreateProduct;
using InventoryManagement.Api.Features.Products.Endpoints.DeleteProduct;
using InventoryManagement.Api.Features.Products.Endpoints.GetProduct;
using InventoryManagement.Api.Features.Products.Endpoints.GetProducts;
using InventoryManagement.Api.Features.Products.Endpoints.UpdateProduct;

namespace InventoryManagement.Api.Features.Products;

public static class ProductsEndpoints
{
    public static void MapProducts(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products").RequireAuthorization();

        group.MapCreateProduct();
        group.MapUpdateProduct();
        group.MapDeleteProduct();

        group.MapGetProduct();
        group.MapGetProducts();
    }
}
