using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCardOnAbp.Cards.Dto;

namespace VCardOnAbp.Cards.Validator
{
    public class CardActionInputValidator : AbstractValidator<ActionInput>
    {
        public CardActionInputValidator() { 
            RuleFor(x => x.Value).GreaterThan(0).When(x => x != null);
        }
    }
}
