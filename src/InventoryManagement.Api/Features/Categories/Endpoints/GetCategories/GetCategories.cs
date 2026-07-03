using InventoryManagement.Api.Data;
using InventoryManagement.Api.Features.Categories.Constant;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Categories.Endpoints.GetCategories;

public static class GetCategories
{
    public static void MapGetCategories(this IEndpointRouteBuilder group)
    {
        group
            .MapGet(
                "/",
                async (AppDbContext db) =>
                {
                    return await db
                        .Categories.Select(c => new GetCategoriesResponse(c.Id, c.Name))
                        .AsNoTracking()
                        .ToListAsync();
                }
            )
            .WithName(CategoryEndpointNames.GetCategory)
            .Produces<List<GetCategoriesResponse>>();
    }
}
