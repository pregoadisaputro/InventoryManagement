using FluentValidation;

namespace InventoryManagement.Api.Features.Suppliers.Endpoints.UpdateSupplier;

public class UpdateSupplierValidator : AbstractValidator<UpdateSupplierRequest>
{
    public UpdateSupplierValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(50);

        RuleFor(x => x.PhoneNumber).NotEmpty().MinimumLength(3).MaximumLength(50);

        RuleFor(x => x.Email).NotEmpty().MinimumLength(3).MaximumLength(50);

        RuleFor(x => x.Address).NotEmpty().MinimumLength(3).MaximumLength(50);
    }
}
