using FluentValidation;

namespace InventoryManagement.Api.Features.Authentication.Endpoints.Login;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Username).NotEmpty();

        RuleFor(x => x.Password).NotEmpty();
    }
}
