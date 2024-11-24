using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCardOnAbp.Models;
using VCardOnAbp.Payments.Dtos;
using VCardOnAbp.Permissions;
using VCardOnAbp.Security;
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
        input.SanitizeInput();
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
        input.SanitizeInput();
        PaymentMethod gateway = await (await _paymentMethodRepository.GetQueryableAsync()).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new UserFriendlyException(L["NotFound", L["Gateway"]]);

        return gateway.GatewayType == GatewayType.MANUAL ?
            await CreateManualTransaction(input, gateway) :
            await CreateAutoTransaction(input, gateway);
    }


    [Authorize(VCardOnAbpPermissions.ViewDepositTransaction)]
    public async Task<PagedResultDto<DepositTransactionDto>> GetDepositTransactions(GetDepositTransactionInput input)
    {
        input.SanitizeInput();
        IQueryable<DepositTransaction> query = (await _depositTransactionRepository.GetQueryableAsync()).AsNoTracking()
            .Where(x => x.Requester == CurrentUser.Id)
            .WhereIf(!string.IsNullOrEmpty(input.Filter), x => EF.Functions.Like(input.Filter, $"%{input.Filter}%"))
            .WhereIf(input.StartDate.HasValue, x => x.CreationTime >= input.StartDate)
            .WhereIf(input.EndDate.HasValue, x => x.CreationTime <= input.EndDate)
            .WhereIf(input.Status != null, x => input.Status!.Any(y => y == x.TransactionStatus));

        List<DepositTransaction> data = await query
            .OrderByDescending(x => x.CreationTime)
            .PageBy(input)
            .ToListAsync();

        return new PagedResultDto<DepositTransactionDto>(
            await query.CountAsync(),
            ObjectMapper.Map<List<DepositTransaction>, List<DepositTransactionDto>>(data)
        );
    }

    [Authorize(VCardOnAbpPermissions.ApproveTransaction)]
    public async Task<ResponseModel> ApproveTransaction(Guid id)
    {
        DepositTransaction transaction = await (await _depositTransactionRepository.GetQueryableAsync())
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new UserFriendlyException(L["NotFound", L["Transaction"]]);

        if (transaction.TransactionStatus != DepositTransactionStatus.Pending) throw new UserFriendlyException(L["TransactionAlreadyProcessed"]);

        IdentityUser user = await _identityUserManager.GetByIdAsync(transaction.Requester);

        // calculate balance by percenage fee and fixed fee by payment gateway
        PaymentMethod gateway = await (await _paymentMethodRepository.GetQueryableAsync())
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == transaction.PaymentMethodId)
            ?? throw new UserFriendlyException(L["NotFound", L["Gateway"]]);

        decimal fee = transaction.Amount * gateway.PercentageFee / 100 + gateway.FixedFee;

        user.ExtraProperties["Balance"] = Convert.ToDecimal(user.ExtraProperties.GetOrDefault("Balance")) + transaction.Amount - fee;
        transaction.FinishTransaction();
        // TODO: Send email to user

        return ResponseModel.SuccessResponse(L["SuccessToast", L["TransactionApproved"]]);
    }

    [Authorize(VCardOnAbpPermissions.ApproveTransaction)]
    public async Task<ResponseModel> RejectTransaction(Guid id)
    {
        DepositTransaction transaction = await (await _depositTransactionRepository.GetQueryableAsync())
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new UserFriendlyException(L["NotFound", L["Transaction"]]);

        if (transaction.TransactionStatus != DepositTransactionStatus.Pending) throw new UserFriendlyException(L["TransactionAlreadyProcessed"]);

        transaction.CancelTransaction();

        // TODO: Send email to user

        return ResponseModel.SuccessResponse(L["SuccessToast", L["TransactionRejected"]]);
    }

    [Authorize(VCardOnAbpPermissions.ApproveTransaction)]
    public async Task<PagedResultDto<DepositTransactionDto>> GetPendingTransactions(GetDepositTransactionInput input)
    {
        input.SanitizeInput();
        IQueryable<DepositTransaction> query = (await _depositTransactionRepository.GetQueryableAsync()).AsNoTracking()
            .WhereIf(!string.IsNullOrEmpty(input.Filter), x => EF.Functions.Like(input.Filter, $"%{input.Filter}%"))
            .WhereIf(input.StartDate.HasValue, x => x.CreationTime >= input.StartDate)
            .WhereIf(input.EndDate.HasValue, x => x.CreationTime <= input.EndDate)
            .WhereIf(input.Status != null, x => input.Status!.Any(y => y == x.TransactionStatus));

        List<DepositTransaction> data = await query
            .OrderByDescending(x => x.CreationTime)
            .PageBy(input)
            .ToListAsync();

        return new PagedResultDto<DepositTransactionDto>(
            await query.CountAsync(),
            ObjectMapper.Map<List<DepositTransaction>, List<DepositTransactionDto>>(data)
        );
    }

    #region Private Methods

    private async Task<CreateDepositTransactionDto> CreateManualTransaction(CreateDepositTransactionInput input, PaymentMethod gateway)
    {
        // Create a manual transaction
        DepositTransaction transaction = new(GuidGenerator.Create(), gateway.Id, input.Amount, CurrentUser.Id!.Value);
        await _depositTransactionRepository.InsertAsync(transaction);

        return ObjectMapper.Map<DepositTransaction, CreateDepositTransactionDto>(transaction);
    }
    private async Task<CreateDepositTransactionDto> CreateAutoTransaction(CreateDepositTransactionInput input, PaymentMethod gateway)
    {
        // Create a manual transaction
        DepositTransaction transaction = new(GuidGenerator.Create(), gateway.Id, input.Amount, CurrentUser.Id!.Value);
        await _depositTransactionRepository.InsertAsync(transaction);

        return ObjectMapper.Map<DepositTransaction, CreateDepositTransactionDto>(transaction);
    }




    #endregion
}

