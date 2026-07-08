using InventoryManagement.Api.Data;
using InventoryManagement.Api.Features.Suppliers.Constant;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Suppliers.Endpoints.GetSupplier;

public static class GetSupplier
{
    public static void MapGetSupplier(this IEndpointRouteBuilder group)
    {
        group
            .MapGet(
                "/{id:int}",
                async (int id, AppDbContext db, CancellationToken cancellationToken) =>
                {
                    var existingSupplier = await db
                        .Suppliers.AsNoTracking()
                        .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

                    if (existingSupplier is null)
                        return Results.NotFound($"Supplier with ID {id} does not exist.");

                    return Results.Ok(
                        new GetSupplierResponse(
                            existingSupplier.Id,
                            existingSupplier.Name,
                            existingSupplier.PhoneNumber,
                            existingSupplier.Email,
                            existingSupplier.Address
                        )
                    );
                }
            )
            .WithName(SupplierEndpointNames.GetSupplierById);
    }
}
