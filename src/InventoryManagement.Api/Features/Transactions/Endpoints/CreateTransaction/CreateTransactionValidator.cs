using FluentValidation;

namespace InventoryManagement.Api.Features.Transactions.Endpoints.CreateTransaction;

public class CreateTransactionValidator : AbstractValidator<CreateTransactionRequest>
{
    public CreateTransactionValidator()
    {
        RuleFor(x => x.Notes).MaximumLength(100);

        RuleFor(x => x.Quantity).NotEmpty().GreaterThan(0);

        RuleFor(x => x.Type).NotEmpty();
    }
}
