using InventoryManagement.Api.Features.Categories.Endpoints.CreateCategory;

namespace InventoryManagement.Api.Features.Categories;

public static class CategoriesEndpoints
{
    public static void MapCategories(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/categories");

        group.MapCreateCategory();
    }
}
