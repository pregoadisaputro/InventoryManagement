using FluentValidation;

namespace InventoryManagement.Api.Features.Products.Endpoints.CreateProduct;

public class CreateProductValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(50);

        RuleFor(x => x.Sku).NotEmpty().MinimumLength(3).MaximumLength(50);

        RuleFor(x => x.Description).MinimumLength(3).MaximumLength(50);

        RuleFor(x => x.Price).NotEmpty().GreaterThan(0);

        RuleFor(x => x.Stock).NotEmpty().GreaterThan(0);

        RuleFor(x => x.MinimumStock).NotEmpty().GreaterThan(0);

        RuleFor(x => x.CategoryId).NotEmpty().GreaterThan(0);

        RuleFor(x => x.SupplierId).GreaterThan(0);
    }
}
