using FluentValidation;

namespace InventoryManagement.Api.Features.Categories.Endpoints.CreateCategory;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
    }
}
