using FluentValidation;
using VCardOnAbp.Cards.Dto;

namespace VCardOnAbp.Cards.Validator
{
    public class CardActionInputValidator : AbstractValidator<ActionInput>
    {
        public CardActionInputValidator()
        {
            RuleFor(x => x.Value).GreaterThan(0).When(x => x != null);
        }
    }
}
