using InventoryManagement.Api.Data;
using InventoryManagement.Api.Features.Suppliers.Constant;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Suppliers.Endpoints.GetSuppliers;

public static class GetSuppliers
{
    public static void MapGetSuppliers(this IEndpointRouteBuilder group)
    {
        group
            .MapGet(
                "/",
                async (AppDbContext db, CancellationToken cancellationToken) =>
                {
                    return await db
                        .Suppliers.Select(s => new GetSuppliersResponse(
                            s.Id,
                            s.Name,
                            s.PhoneNumber,
                            s.Email,
                            s.Address
                        ))
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);
                }
            )
            .WithName(SupplierEndpointNames.GetSuppliers)
            .Produces<GetSuppliersResponse>(StatusCodes.Status200OK);
    }
}
