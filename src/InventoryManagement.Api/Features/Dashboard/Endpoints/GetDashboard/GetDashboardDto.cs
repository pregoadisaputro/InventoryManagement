namespace InventoryManagement.Api.Features.Dashboard.Endpoints.GetDashboard;

public record GetDashboardResponse(
    int TotalProducts,
    IEnumerable<GetDashboardAlertsResponse> LowStockProducts,
    IEnumerable<GetDashboardAlertsResponse> OutOfStockProducts,
    decimal TotalInventoryValue,
    int TotalCategories,
    int TotalSuppliers
);

public record GetDashboardAlertsResponse(int Id, string Name, int Stock, int MinimumStock);
