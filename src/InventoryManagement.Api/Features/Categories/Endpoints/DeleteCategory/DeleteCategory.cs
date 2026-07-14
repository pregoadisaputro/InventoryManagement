using InventoryManagement.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Categories.Endpoints.DeleteCategory;

public static class DeleteCategory
{
    public static void MapDeleteCategory(this IEndpointRouteBuilder group)
    {
        group
            .MapDelete(
                "/{id:int}",
                async (int id, AppDbContext db, CancellationToken cancellationToken) =>
                {
                    var category = await db.Categories.FindAsync([id], cancellationToken);

                    if (category is null)
                    {
                        return Results.NotFound($"Category with ID {id} was not found.");
                    }

                    var hasProducts = await db.Products.AnyAsync(
                        p => p.CategoryId == id,
                        cancellationToken
                    );

                    if (hasProducts)
                    {
                        return Results.Conflict(
                            "Cannot delete category because it is used by one or more products."
                        );
                    }

                    db.Categories.Remove(category);
                    await db.SaveChangesAsync(cancellationToken);

                    return Results.NoContent();
                }
            )
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status204NoContent);
    }
}
