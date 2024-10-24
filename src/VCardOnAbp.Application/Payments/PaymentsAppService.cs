using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCardOnAbp.Payments.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace VCardOnAbp.Payments;
public class PaymentsAppService(
    IRepository<PaymentMethod, int> paymentMethodRepository
) : VCardOnAbpAppService, IPaymentsAppService
{
    private readonly IRepository<PaymentMethod, int> _paymentMethodRepository = paymentMethodRepository;
    public async Task<PagedResultDto<PaymentMethodDto>> GetPaymentMethodsAsync(GetPaymentMethodsInput input)
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
}
