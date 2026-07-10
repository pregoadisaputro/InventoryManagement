namespace InventoryManagement.Api.Features.Users.Endpoints.GetUser;

public record GetUserResponse(int Id, string Username, DateTimeOffset CreatedAt);
