using InventoryManagement.Api.Data;
using InventoryManagement.Api.Features.Categories.Constant;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Categories.Endpoints.GetCategory;

public static class GetCategoryById
{
    public static void MapGetCategoryById(this IEndpointRouteBuilder group)
    {
        group
            .MapGet(
                "/{id:int}",
                async (int Id, AppDbContext db, CancellationToken cancellationToken) =>
                {
                    var existingCategory = await db
                        .Categories.AsNoTracking()
                        .FirstOrDefaultAsync(c => c.Id == Id, cancellationToken);

                    if (existingCategory is null)
                    {
                        return Results.BadRequest($"Category with ID {Id} not exist.");
                    }

                    return Results.Ok(
                        new GetCategoryByIdResponse(existingCategory.Id, existingCategory.Name)
                    );
                }
            )
            .WithName(CategoryEndpointNames.GetCategoryById);
    }
}
