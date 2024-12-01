using System.Threading.Tasks;
using VCardOnAbp.Accounts.Dtos;
using VCardOnAbp.Models;
using Volo.Abp.Account;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace VCardOnAbp.Accounts;
public interface IAccountsAppService : IProfileAppService, ITransientDependency
{
    public Task<PagedResultDto<UserTransactionDto>> GetTransactionsAsync(GetUserTransactionInput input);
    public Task<ProfileInfoDto> GetProfileAsync();
    public Task<ResponseModel> CreateTwoFactorKeyAsync();
    public Task<ResponseModel> Verify2FA(string otp);
}
