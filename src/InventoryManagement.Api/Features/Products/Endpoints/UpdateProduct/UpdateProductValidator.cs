using FluentValidation;

namespace InventoryManagement.Api.Features.Products.Endpoints.UpdateProduct;

public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(50);

        RuleFor(x => x.Sku).NotEmpty().MinimumLength(3).MaximumLength(50);

        RuleFor(x => x.Description).MinimumLength(3).MaximumLength(100);

        RuleFor(x => x.Price).NotEmpty().GreaterThan(0);

        RuleFor(x => x.MinimumStock).NotEmpty().GreaterThanOrEqualTo(0);

        RuleFor(x => x.CategoryId).NotEmpty().GreaterThan(0);

        RuleFor(x => x.SupplierId).GreaterThan(0);
    }
}
