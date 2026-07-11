using FluentValidation;

namespace InventoryManagement.Api.Features.Suppliers.Endpoints.CreateSupplier;

public class CreateSupplierValidator : AbstractValidator<CreateSupplierRequest>
{
    public CreateSupplierValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(50);

        RuleFor(x => x.PhoneNumber).NotEmpty().MinimumLength(3).MaximumLength(50);

        RuleFor(x => x.Email).NotEmpty().MinimumLength(3).MaximumLength(50);

        RuleFor(x => x.Address).NotEmpty().MinimumLength(3).MaximumLength(50);
    }
}
