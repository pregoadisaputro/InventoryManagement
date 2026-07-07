using InventoryManagement.Api.Features.Suppliers.Endpoints.CreateSupplier;

namespace InventoryManagement.Api.Features.Suppliers;

public static class SuppliersEndpoints
{
    public static void MapSuppliers(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/suppliers");

        group.MapCreateSupplier();
    }
}
