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
                    var response = await db
                        .Categories.AsNoTracking()
                        .Where(c => c.Id == id)
                        .Select(c => new GetCategoryResponse(c.Id, c.Name))
                        .FirstOrDefaultAsync(cancellationToken);

                    if (response is null)
                    {
                        return Results.NotFound($"Category with ID {id} was not found.");
                    }

                    return Results.Ok(response);
                }
            )
            .WithName(CategoryEndpointNames.GetCategory);
    }
}
