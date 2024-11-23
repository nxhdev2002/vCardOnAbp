using System;
using System.Threading.Tasks;
using VCardOnAbp.Payments.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace VCardOnAbp.Payments;
public interface IPaymentsAppService : IApplicationService
{
    Task<PagedResultDto<PaymentMethodDto>> GetPaymentMethodsAsync(GetPaymentMethodsInput input);
    Task<CreateDepositTransactionDto> CreateTransaction(int id, CreateDepositTransactionInput input);
    Task UpdateTransaction(int id, Guid transId, ProcessTransactionInput input);
    Task<PagedResultDto<DepositTransactionDto>> GetDepositTransactions(GetDepositTransactionInput input);
}