using FluentValidation;

namespace InventoryManagement.Api.Features.Categories.Endpoints.UpdateCategory;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(50);
    }
}
