using InventoryManagement.Api.Data;
using InventoryManagement.Api.Data.Entity;
using InventoryManagement.Api.Features.Suppliers.Constant;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Suppliers.Endpoints.CreateSupplier;

public static class CreateSupplier
{
    public static void MapCreateSupplier(this IEndpointRouteBuilder group)
    {
        group.MapPost(
            "/",
            async (
                CreateSupplierRequest request,
                AppDbContext db,
                CancellationToken cancellationToken
            ) =>
            {
                var existingSupplier = await db
                    .Suppliers.AsNoTracking()
                    .AnyAsync(s => EF.Functions.ILike(s.Name, request.Name), cancellationToken);

                if (existingSupplier)
                    return Results.Conflict($"Supplier with name {request.Name} already exist.");

                var newSupplier = new Supplier
                {
                    Name = request.Name,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    Address = request.Address,
                };

                db.Suppliers.Add(newSupplier);
                await db.SaveChangesAsync(cancellationToken);

                return Results.CreatedAtRoute(
                    SupplierEndpointNames.GetSupplierById,
                    new { id = newSupplier.Id },
                    new CreateSupplierResponse(
                        newSupplier.Id,
                        newSupplier.Name,
                        newSupplier.PhoneNumber,
                        newSupplier.Email,
                        newSupplier.Address
                    )
                );
            }
        );
    }
}
