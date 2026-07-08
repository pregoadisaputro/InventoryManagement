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
                    var response = await db
                        .Suppliers.AsNoTracking()
                        .Where(s => s.Id == id)
                        .Select(s => new GetSupplierResponse(
                            s.Id,
                            s.Name,
                            s.PhoneNumber,
                            s.Email,
                            s.Address
                        ))
                        .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

                    if (response is null)
                    {
                        return Results.NotFound($"Supplier with ID {id} was not found.");
                    }

                    return Results.Ok(response);
                }
            )
            .WithName(SupplierEndpointNames.GetSupplier);
    }
}
