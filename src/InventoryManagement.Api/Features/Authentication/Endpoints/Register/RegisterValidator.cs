using FluentValidation;

namespace InventoryManagement.Api.Features.Authentication.Endpoints.Register;

public class RegisterValidator : AbstractValidator<RegisterRequest>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Username must be 50 characters or fewer.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long.");
    }
}
