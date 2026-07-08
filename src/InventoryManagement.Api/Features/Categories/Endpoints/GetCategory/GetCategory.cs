using InventoryManagement.Api.Data;
using InventoryManagement.Api.Features.Categories.Constant;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Categories.Endpoints.GetCategory;

public static class GetCategory
{
    public static void MapGetCategory(this IEndpointRouteBuilder group)
    {
        group
            .MapGet(
                "/{id:int}",
                async (int id, AppDbContext db, CancellationToken cancellationToken) =>
                {
                    var existingCategory = await db
                        .Categories.AsNoTracking()
                        .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

                    if (existingCategory is null)
                        return Results.NotFound($"Category with ID {id} not exist.");

                    return Results.Ok(
                        new GetCategoryResponse(existingCategory.Id, existingCategory.Name)
                    );
                }
            )
            .WithName(CategoryEndpointNames.GetCategory);
    }
}
