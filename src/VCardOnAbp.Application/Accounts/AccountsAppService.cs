using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCardOnAbp.Accounts.Dtos;
using VCardOnAbp.Transactions;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
namespace VCardOnAbp.Accounts;

[ExposeServices(typeof(ProfileAppService), typeof(IProfileAppService))]
[Dependency(ReplaceServices = true)]
public class AccountsAppService : ProfileAppService, IAccountsAppService
{
    private readonly IRepository<UserTransaction, Guid> _userTransactionRepository;

    public AccountsAppService(
        IdentityUserManager userManager,
        IOptions<IdentityOptions> identityOptions,
        IRepository<UserTransaction, Guid> userTransactionRepository
    ) : base(userManager, identityOptions)
    {
        _userTransactionRepository = userTransactionRepository;
    }

    public override Task<ProfileDto> UpdateAsync(UpdateProfileDto input)
    {
        input.ExtraProperties.Remove("Balance");
        return base.UpdateAsync(input);
    }

    [RemoteService(false)]
    public async Task<PagedResultDto<UserTransactionDto>> GetTransactionsAsync(GetUserTransactionInput input)
    {
        IOrderedQueryable<UserTransaction> query = (await _userTransactionRepository.GetQueryableAsync())
            .Where(x => x.UserId == CurrentUser.Id!.Value)
            .OrderByDescending(x => x.CreationTime);

        List<UserTransaction> data = await query
            .WhereIf(!string.IsNullOrEmpty(input.Filter), x => EF.Functions.Like(x.Description, $"%{input.Filter}%"))
            .PageBy(input)
            .ToListAsync();

        return new PagedResultDto<UserTransactionDto>(
            await query.CountAsync(),
            ObjectMapper.Map<List<UserTransaction>, List<UserTransactionDto>>(data)
        );
    }
}
