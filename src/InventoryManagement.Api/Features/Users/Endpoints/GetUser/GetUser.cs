using InventoryManagement.Api.Data;
using InventoryManagement.Api.Features.Users.Constant;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Users.Endpoints.GetUser;

public static class GetUser
{
    public static void MapGetUser(this IEndpointRouteBuilder group)
    {
        group
            .MapGet(
                "/{id:int}",
                async (int id, AppDbContext db, CancellationToken cancellationToken) =>
                {
                    var response = await db
                        .Users.AsNoTracking()
                        .Where(u => u.Id == id)
                        .Select(u => new GetUserResponse(u.Id, u.Username, u.CreatedAt))
                        .FirstOrDefaultAsync(cancellationToken);

                    if (response is null)
                    {
                        return Results.NotFound($"User with ID {id} was not found.");
                    }

                    return Results.Ok(response);
                }
            )
            .WithName(UserEndpointsNames.GetUser)
            .Produces<GetUserResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }
}
