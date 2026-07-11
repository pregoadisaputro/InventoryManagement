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
                    await db
                        .Categories.Where(c => c.Id == id)
                        .ExecuteDeleteAsync(cancellationToken);

                    return Results.NoContent();
                }
            )
            .Produces(StatusCodes.Status204NoContent);
    }
}
