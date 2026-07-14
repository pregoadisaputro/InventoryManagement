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
                    var supplier = await db.Suppliers.FindAsync([id], cancellationToken);

                    if (supplier is null)
                    {
                        return Results.NotFound($"Supplier with ID {id} was not found.");
                    }

                    var hasProducts = await db.Products.AnyAsync(
                        p => p.SupplierId == id,
                        cancellationToken
                    );

                    if (hasProducts)
                    {
                        return Results.Conflict(
                            "Cannot delete supplier because it is used by one or more products."
                        );
                    }

                    db.Suppliers.Remove(supplier);
                    await db.SaveChangesAsync(cancellationToken);

                    return Results.NoContent();
                }
            )
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status204NoContent);
    }
}
