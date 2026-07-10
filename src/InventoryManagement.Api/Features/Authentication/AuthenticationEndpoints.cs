using InventoryManagement.Api.Features.Authentication.Endpoints.Login;
using InventoryManagement.Api.Features.Authentication.Endpoints.Register;

namespace InventoryManagement.Api.Features.Authentication;

public static class AuthenticationEndpoints
{
    public static void MapAuthentication(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth");

        group.MapRegister();
        group.MapLogin();
    }
}
