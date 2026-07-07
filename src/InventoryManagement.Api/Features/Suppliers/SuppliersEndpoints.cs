using InventoryManagement.Api.Features.Suppliers.Endpoints.CreateSupplier;
using InventoryManagement.Api.Features.Suppliers.Endpoints.GetSupplier;

namespace InventoryManagement.Api.Features.Suppliers;

public static class SuppliersEndpoints
{
    public static void MapSuppliers(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/suppliers");

        group.MapCreateSupplier();
        group.MapGetSupplier();
    }
}
