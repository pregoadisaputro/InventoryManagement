namespace InventoryManagement.Api.Features.Dashboard.Endpoints.GetDashboard;

public record GetDashboardResponse(
    int TotalProducts,
    IReadOnlyList<GetDashboardAlertsResponse> LowStockProducts,
    IReadOnlyList<GetDashboardAlertsResponse> OutOfStockProducts,
    decimal TotalInventoryValue,
    int TotalCategories,
    int TotalSuppliers
);

public record GetDashboardAlertsResponse(int Id, string Name, int Stock, int MinimumStock);
