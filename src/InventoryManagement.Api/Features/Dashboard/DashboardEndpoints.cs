using InventoryManagement.Api.Features.Dashboard.Endpoints.GetDashboard;

namespace InventoryManagement.Api.Features.Dashboard;

public static class DashboardEndpoints
{
    public static void MapDashboard(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/dashboard");

        group.MapGetDashboard();
    }
}
