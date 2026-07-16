using FluentValidation;
using InventoryManagement.Api.Data;
using InventoryManagement.Api.Data.Entity;
using InventoryManagement.Api.Features.Users.Constant;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Authentication.Endpoints.Register;

public static class Register
{
    public static void MapRegister(this IEndpointRouteBuilder group)
    {
        group
            .MapPost(
                "/register",
                async (
                    RegisterRequest request,
                    AppDbContext db,
                    IValidator<RegisterRequest> valdiator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var validationResult = await valdiator.ValidateAsync(
                        request,
                        cancellationToken
                    );

                    if (!validationResult.IsValid)
                    {
                        return Results.ValidationProblem(validationResult.ToDictionary());
                    }

                    var username = request.Username.Trim();

                    var existingUsername = await db
                        .Users.AsNoTracking()
                        .AnyAsync(u => EF.Functions.ILike(u.Username, username), cancellationToken);

                    if (existingUsername)
                    {
                        return Results.Json(
                            new { title = "Username is already taken." },
                            statusCode: StatusCodes.Status409Conflict
                        );
                    }

                    var newUser = new User
                    {
                        Username = username,
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    };

                    db.Users.Add(newUser);
                    await db.SaveChangesAsync(cancellationToken);

                    return Results.CreatedAtRoute(
                        UserEndpointsNames.GetUser,
                        new { id = newUser.Id },
                        new RegisterResponse(newUser.Id, newUser.Username, newUser.CreatedAt)
                    );
                }
            )
            .Produces<RegisterResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status409Conflict);
    }
}
