using System.Threading.Tasks;
using VCardOnAbp.Payments.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace VCardOnAbp.Payments;
public interface IPaymentsAppService : IApplicationService
{
    Task<PagedResultDto<PaymentMethodDto>> GetPaymentMethodsAsync(GetPaymentMethodsInput input);
}