namespace InventoryManagement.Api.Features.Suppliers.Endpoints.GetSupplier;

public record GetSupplierResponse(
    int Id,
    string Name,
    string PhoneNumber,
    string Email,
    string Address
);
