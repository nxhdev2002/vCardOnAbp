using System;
using System.Threading.Tasks;
using VCardOnAbp.Models;
using VCardOnAbp.Payments.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace VCardOnAbp.Payments;
public interface IPaymentsAppService : IApplicationService
{
    Task<PagedResultDto<PaymentMethodDto>> GetPaymentMethodsAsync(GetPaymentMethodsInput input);
    Task<ResponseModel> CreatePaymentMethodsAsync(CreatePaymentMethodInput input);
    Task<CreateDepositTransactionDto> CreateTransaction(int id, CreateDepositTransactionInput input);
    Task<PagedResultDto<DepositTransactionDto>> GetDepositTransactions(GetDepositTransactionInput input);
    Task<ResponseModel> ApproveTransaction(Guid id);
    Task<ResponseModel> RejectTransaction(Guid id);
    Task<PagedResultDto<DepositTransactionDto>> GetPendingTransactions(GetDepositTransactionInput input);
}