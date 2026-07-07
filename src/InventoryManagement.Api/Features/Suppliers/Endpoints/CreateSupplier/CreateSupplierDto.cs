namespace InventoryManagement.Api.Features.Suppliers.Endpoints.CreateSupplier;

public record CreateSupplierRequest(string Name, string PhoneNumber, string Email, string Address);

public record CreateSupplierResponse(
    int Id,
    string Name,
    string PhoneNumber,
    string Email,
    string Address
);
