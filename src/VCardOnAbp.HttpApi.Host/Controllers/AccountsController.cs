using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VCardOnAbp.Accounts;
using VCardOnAbp.Accounts.Dtos;
using VCardOnAbp.Models;
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

    [HttpGet]
    [Route("/api/app/account/profile")]
    public Task<ProfileInfoDto> GetProfile()
    {
        return _accountsAppService.GetProfileAsync();
    }

    [HttpPost]
    [Route("/api/app/account/2fa")]
    public Task<ResponseModel> Generate2Fa()
    {
        return _accountsAppService.CreateTwoFactorKeyAsync();
    }

    [HttpPost]
    [Route("/api/app/account/2fa/verify")]
    public Task<ResponseModel> Verify2FA(string otp)
    {
        return _accountsAppService.Verify2FA(otp);
    }
}
