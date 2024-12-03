using FluentValidation;
using VCardOnAbp.Cards.Dto;

namespace VCardOnAbp.Cards.Validator;

public class CreateCardInputValidator : AbstractValidator<CreateCardInput>
{
    public CreateCardInputValidator()
    {
        RuleFor(x => x.Amount).GreaterThanOrEqualTo(VCardOnAbpConsts.MinCreationBalance);
    }
}
