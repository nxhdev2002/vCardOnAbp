using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VCardOnAbp.Accounts;
using VCardOnAbp.Accounts.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace VCardOnAbp.Controllers;

public class AccountsController(
    IAccountsAppService accountsAppService
) : AbpController
{
    private readonly IAccountsAppService _accountsAppService = accountsAppService;

    [HttpGet]
    [Route("/api/app/accounts/transactions")]
    public Task<PagedResultDto<UserTransactionDto>> GetTransactions(GetUserTransactionInput input)
    {
        return _accountsAppService.GetTransactionsAsync(input);
    }
}
