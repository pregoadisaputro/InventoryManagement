namespace InventoryManagement.Api.Features.Dashboard.Endpoints.GetDashboard;

public record GetDashboardResponse(
    int TotalProducts,
    int LowStockProducts,
    int OutOfStockProducts,
    decimal TotalInventoryValue,
    int TotalCategories,
    int TotalSuppliers
);
