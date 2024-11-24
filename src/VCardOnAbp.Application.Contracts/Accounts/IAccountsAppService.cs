using System.Threading.Tasks;
using VCardOnAbp.Accounts.Dtos;
using Volo.Abp.Account;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace VCardOnAbp.Accounts;
public interface IAccountsAppService : IProfileAppService, ITransientDependency
{
    public Task<PagedResultDto<UserTransactionDto>> GetTransactionsAsync(GetUserTransactionInput input);
}
