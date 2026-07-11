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
                        .CountAsync(
                            p => p.Stock > 0 && p.Stock <= p.MinimumStock,
                            cancellationToken
                        );
                    var outOfStockProducts = await db
                        .Products.AsNoTracking()
                        .CountAsync(p => p.Stock == 0, cancellationToken);
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
