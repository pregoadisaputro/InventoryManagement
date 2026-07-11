using InventoryManagement.Api.Features.Suppliers.Endpoints.CreateSupplier;
using InventoryManagement.Api.Features.Suppliers.Endpoints.DeleteSupplier;
using InventoryManagement.Api.Features.Suppliers.Endpoints.GetSupplier;
using InventoryManagement.Api.Features.Suppliers.Endpoints.UpdateSupplier;

namespace InventoryManagement.Api.Features.Suppliers;

public static class SuppliersEndpoints
{
    public static void MapSuppliers(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/suppliers").RequireAuthorization();

        group.MapCreateSupplier();
        group.MapUpdateSupplier();
        group.MapDeleteSupplier();

        group.MapGetSupplier();
    }
}
