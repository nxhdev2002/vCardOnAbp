using FluentValidation;
using VCardOnAbp.Payments.Dtos;

namespace VCardOnAbp.Payments.Validator;
public class CreateDepositTransactionValidator : AbstractValidator<CreateDepositTransactionInput>
{
    private const decimal MinAmount = 10;
    private const decimal MaxAmount = 100;

    public CreateDepositTransactionValidator()
    {
        RuleFor(x => x.Amount).GreaterThanOrEqualTo(MinAmount).LessThanOrEqualTo(MaxAmount);
    }
}
