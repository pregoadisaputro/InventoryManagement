namespace InventoryManagement.Api.Features.Authentication.Endpoints.Register;

public record RegisterRequest(string Username, string Password);

public record RegisterResponse(int Id, string Username, DateTimeOffset CreatedAt);
