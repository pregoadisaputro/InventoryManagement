using InventoryManagement.Api.Features.Categories.Endpoints.CreateCategory;
using InventoryManagement.Api.Features.Categories.Endpoints.GetCategories;
using InventoryManagement.Api.Features.Categories.Endpoints.GetCategory;

namespace InventoryManagement.Api.Features.Categories;

public static class CategoriesEndpoints
{
    public static void MapCategories(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/categories");

        group.MapCreateCategory();
        group.MapGetCategories();
        group.MapGetCategory();
    }
}
