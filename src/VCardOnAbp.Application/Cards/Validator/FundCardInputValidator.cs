using FluentValidation;
using VCardOnAbp.Cards.Dto;

namespace VCardOnAbp.Cards.Validator
{
    public class FundCardInputValidator : AbstractValidator<FundCardInput>
    {
        public FundCardInputValidator()
        {
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
        }
    }
}
