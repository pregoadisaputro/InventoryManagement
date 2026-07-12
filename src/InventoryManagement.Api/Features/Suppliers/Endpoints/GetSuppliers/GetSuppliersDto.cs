namespace InventoryManagement.Api.Features.Suppliers.Endpoints.GetSuppliers;

public record GetSuppliersResponse(
    int Id,
    string Name,
    string PhoneNumber,
    string Email,
    string Address
);
