namespace InventoryManagement.Api.Features.Suppliers.Endpoints.UpdateSupplier;

public record UpdateSupplierRequest(string Name, string PhoneNumber, string Email, string Address);
