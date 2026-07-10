using InventoryManagement.Api.Features.Users.Endpoints.GetUser;

namespace InventoryManagement.Api.Features.Users;

public static class UserEndpoints
{
    public static void MapUsers(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/users").RequireAuthorization();

        group.MapGetUser();
    }
}
