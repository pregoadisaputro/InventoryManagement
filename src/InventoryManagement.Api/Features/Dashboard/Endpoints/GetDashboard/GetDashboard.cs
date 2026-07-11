using InventoryManagement.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Dashboard.Endpoints.GetDashboard;

public static class GetDashboard
{
    public static void MapGetDashboard(this IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/",
                async (AppDbContext db, CancellationToken cancellationToken) =>
                {
                    var totalProducts = await db
                        .Products.AsNoTracking()
                        .CountAsync(cancellationToken);

                    var lowStockProducts = await db
                        .Products.AsNoTracking()
                        .Where(p => p.Stock > 0 && p.Stock <= p.MinimumStock)
                        .OrderBy(p => p.Stock)
                        .Select(p => new GetDashboardAlertsResponse(
                            p.Id,
                            p.Name,
                            p.Stock,
                            p.MinimumStock
                        ))
                        .ToListAsync(cancellationToken);

                    var outOfStockProducts = await db
                        .Products.AsNoTracking()
                        .Where(p => p.Stock == 0)
                        .OrderBy(p => p.Name)
                        .Select(p => new GetDashboardAlertsResponse(
                            p.Id,
                            p.Name,
                            p.Stock,
                            p.MinimumStock
                        ))
                        .ToListAsync(cancellationToken);

                    var totalInventoryValue = await db
                        .Products.AsNoTracking()
                        .SumAsync(p => p.Price * p.Stock, cancellationToken);

                    var totalCategories = await db
                        .Categories.AsNoTracking()
                        .CountAsync(cancellationToken);
                    var totalSuppliers = await db
                        .Suppliers.AsNoTracking()
                        .CountAsync(cancellationToken);

                    return Results.Ok(
                        new GetDashboardResponse(
                            totalProducts,
                            lowStockProducts,
                            outOfStockProducts,
                            totalInventoryValue,
                            totalCategories,
                            totalSuppliers
                        )
                    );
                }
            )
            .Produces<GetDashboardResponse>(StatusCodes.Status200OK);
    }
}
