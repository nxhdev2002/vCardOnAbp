using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCardOnAbp.Payments.Dtos;
using VCardOnAbp.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace VCardOnAbp.Payments;

[Authorize(VCardOnAbpPermissions.PaymentGroup)]
public class PaymentsAppService(
    IRepository<PaymentMethod, int> paymentMethodRepository,
    IRepository<DepositTransaction, Guid> depositTransactionRepository,
    IdentityUserManager identityUserManager
) : VCardOnAbpAppService, IPaymentsAppService
{
    private readonly IRepository<PaymentMethod, int> _paymentMethodRepository = paymentMethodRepository;
    private readonly IRepository<DepositTransaction, Guid> _depositTransactionRepository = depositTransactionRepository;
    private readonly IdentityUserManager _identityUserManager = identityUserManager;

    [Authorize(VCardOnAbpPermissions.ViewPayment)]
    public virtual async Task<PagedResultDto<PaymentMethodDto>> GetPaymentMethodsAsync(GetPaymentMethodsInput input)
    {
        IQueryable<PaymentMethod> payments = (await _paymentMethodRepository.GetQueryableAsync()).AsNoTracking()
            .WhereIf(!string.IsNullOrEmpty(input.Filter), x => EF.Functions.Like(input.Filter, $"%{input.Filter}%"));

        int totalCount = await payments.CountAsync();

        List<PaymentMethod> data = await payments
            .PageBy(input)
            .ToListAsync();

        return new PagedResultDto<PaymentMethodDto>(
            totalCount,
            ObjectMapper.Map<List<PaymentMethod>, List<PaymentMethodDto>>(data)
        );
    }

    [Authorize(VCardOnAbpPermissions.Deposit)]
    public virtual async Task<CreateDepositTransactionDto> CreateTransaction(int id, CreateDepositTransactionInput input)
    {
        var gateway = await (await _paymentMethodRepository.GetQueryableAsync()).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id) 
            ?? throw new UserFriendlyException(L["NotFound", L["Gateway"]]);

        return gateway.GatewayType == GatewayType.MANUAL ? 
            await CreateManualTransaction(input, gateway) : 
            await CreateAutoTransaction(input, gateway);
    }

    [Authorize(VCardOnAbpPermissions.ProcessDeposit)]
    public virtual async Task UpdateTransaction(int id, Guid transId, ProcessTransactionInput input)
    {
        var transaction = await (await _depositTransactionRepository.GetQueryableAsync())
            .FirstOrDefaultAsync(x => x.Id == transId && x.PaymentMethodId == id) ?? throw new UserFriendlyException(L["NotFound", L["Transaction"]]);
        
        transaction.ConcurrencyStamp = input.ConcurrencyStamp;
        if (transaction.TransactionStatus != DepositTransactionStatus.Pending) throw new UserFriendlyException(L["TransactionAlreadyProcessed"]);
 
        if (!string.IsNullOrEmpty(input.Comment)) transaction.SetComment(input.Comment);

        if (input.Status == DepositTransactionStatus.Completed)
        {
            var user = await _identityUserManager.GetByIdAsync(transaction.Requester);
            user.ExtraProperties["Balance"] = Convert.ToDecimal(user.ExtraProperties.GetOrDefault("Balance")) + transaction.Amount;
            transaction.FinishTransaction();


        }
        else if (input.Status == DepositTransactionStatus.Failed) transaction.CancelTransaction();
        else throw new UserFriendlyException(L["InvalidStatus"]);

        await _depositTransactionRepository.UpdateAsync(transaction, autoSave: true);
    }

    #region Private Methods

    private async Task<CreateDepositTransactionDto> CreateManualTransaction(CreateDepositTransactionInput input, PaymentMethod gateway)
    {
        // Create a manual transaction
        var transaction = new DepositTransaction(GuidGenerator.Create(), gateway.Id, input.Amount, CurrentUser.Id!.Value);
        await _depositTransactionRepository.InsertAsync(transaction);

        return ObjectMapper.Map<DepositTransaction, CreateDepositTransactionDto>(transaction);
    }
    private async Task<CreateDepositTransactionDto> CreateAutoTransaction(CreateDepositTransactionInput input, PaymentMethod gateway)
    {
        // Create a manual transaction
        var transaction = new DepositTransaction(GuidGenerator.Create(), gateway.Id, input.Amount, CurrentUser.Id!.Value);
        await _depositTransactionRepository.InsertAsync(transaction);

        return ObjectMapper.Map<DepositTransaction, CreateDepositTransactionDto>(transaction);
    }

    #endregion
}

