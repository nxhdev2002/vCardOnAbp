using FluentValidation;
using VCardOnAbp.Bins.Dtos;

namespace VCardOnAbp.Bins.Validator;
public class CreateBinInputValidator : AbstractValidator<CreateBinDtoInput>
{
    public CreateBinInputValidator()
    {
        RuleFor(x => x.CreationPercentFee).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);
        RuleFor(x => x.CreationFixedFee).GreaterThanOrEqualTo(0);
        RuleFor(x => x.FundingPercentFee).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);
        RuleFor(x => x.FundingFixedFee).GreaterThanOrEqualTo(0);

        RuleFor(x => x.Name).MaximumLength(VCardOnAbpConsts.MaxNameLength);
        RuleFor(x => x.Description).MaximumLength(VCardOnAbpConsts.MaxDescriptionLength);
    }
}
