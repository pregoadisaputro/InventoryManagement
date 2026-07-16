using FluentValidation;
using InventoryManagement.Api.Data;
using InventoryManagement.Api.Features.Authentication.Constant;
using InventoryManagement.Api.Features.Authentication.Services;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Authentication.Endpoints.Login;

public static class Login
{
    public static void MapLogin(this IEndpointRouteBuilder group)
    {
        group
            .MapPost(
                "/login",
                async (
                    LoginRequest request,
                    IValidator<LoginRequest> validator,
                    IJwtService jwtService,
                    AppDbContext db,
                    CancellationToken cancellationToken
                ) =>
                {
                    var validationResult = await validator.ValidateAsync(
                        request,
                        cancellationToken
                    );

                    if (!validationResult.IsValid)
                    {
                        return Results.ValidationProblem(validationResult.ToDictionary());
                    }

                    var username = request.Username.Trim();

                    var user = await db
                        .Users.AsNoTracking()
                        .FirstOrDefaultAsync(
                            u => EF.Functions.ILike(u.Username, username),
                            cancellationToken
                        );

                    if (user is null)
                    {
                        return Results.Json(
                            new { title = "Invalid username or password" },
                            statusCode: StatusCodes.Status401Unauthorized
                        );
                    }

                    var validPassword = BCrypt.Net.BCrypt.Verify(
                        request.Password,
                        user.PasswordHash
                    );

                    if (!validPassword)
                    {
                        return Results.Json(
                            new { title = "Invalid Password or password" },
                            statusCode: StatusCodes.Status401Unauthorized
                        );
                    }

                    var token = jwtService.GenerateToken(user);

                    return Results.Ok(
                        new LoginResponse(user.Username, token, jwtService.ExpiresAt)
                    );
                }
            )
            .WithName(AuthEndpointNames.UserLogin)
            .Produces<LoginResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status401Unauthorized);
    }
}
