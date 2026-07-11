using InventoryManagement.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Suppliers.Endpoints.DeleteSupplier;

public static class DeleteSupplier
{
    public static void MapDeleteSupplier(this IEndpointRouteBuilder group)
    {
        group
            .MapDelete(
                "/{id:int}",
                async (int id, AppDbContext db, CancellationToken cancellationToken) =>
                {
                    await db.Suppliers.Where(s => s.Id == id).ExecuteDeleteAsync(cancellationToken);

                    return Results.NoContent();
                }
            )
            .Produces(StatusCodes.Status204NoContent);
    }
}
