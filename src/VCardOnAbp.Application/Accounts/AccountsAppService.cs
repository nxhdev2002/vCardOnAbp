using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace VCardOnAbp.Accounts;

[ExposeServices(typeof(ProfileAppService), typeof(IProfileAppService))]
[Dependency(ReplaceServices = true)]
public class AccountsAppService : ProfileAppService
{
    public AccountsAppService(IdentityUserManager userManager, IOptions<IdentityOptions> identityOptions) : base(userManager, identityOptions)
    {
    }

    public override Task<ProfileDto> UpdateAsync(UpdateProfileDto input)
    {
        input.ExtraProperties.Remove("Balance");
        return base.UpdateAsync(input);
    }
}
