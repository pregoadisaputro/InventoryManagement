using System.Security.Claims;
using InventoryManagement.Api.Data;
using InventoryManagement.Api.Features.Users.Constant;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Users.Endpoints.GetCurrentUser;

public static class GetCurrentUser
{
    public static void MapGetCurrentUser(this IEndpointRouteBuilder group)
    {
        group
            .MapGet(
                "/me",
                async (
                    ClaimsPrincipal user,
                    AppDbContext db,
                    CancellationToken cancellationToken
                ) =>
                {
                    var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

                    if (userIdClaim is null)
                    {
                        return Results.Unauthorized();
                    }

                    var userId = int.Parse(userIdClaim.Value);

                    var response = await db
                        .Users.AsNoTracking()
                        .Where(u => u.Id == userId)
                        .Select(u => new GetCurrentUserResponse(u.Username))
                        .FirstOrDefaultAsync(cancellationToken);

                    return response is null ? Results.NotFound() : Results.Ok(response);
                }
            )
            .WithName(UserEndpointsNames.GetCurrentUser)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .Produces<GetCurrentUserResponse>(StatusCodes.Status200OK);
    }
}
