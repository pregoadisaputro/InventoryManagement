namespace InventoryManagement.Api.Features.Authentication.Endpoints.Login;

public record LoginRequest(string Username, string Password);

public record LoginResponse(string Token);
