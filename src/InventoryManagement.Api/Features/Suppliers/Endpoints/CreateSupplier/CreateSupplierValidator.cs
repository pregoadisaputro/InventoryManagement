using FluentValidation;

namespace InventoryManagement.Api.Features.Suppliers.Endpoints.CreateSupplier;

public class CreateSupplierValidator : AbstractValidator<CreateSupplierRequest>
{
    public CreateSupplierValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);

        RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(50);

        RuleFor(x => x.Email).NotEmpty().MaximumLength(50);

        RuleFor(x => x.Address).NotEmpty().MaximumLength(50);
    }
}
