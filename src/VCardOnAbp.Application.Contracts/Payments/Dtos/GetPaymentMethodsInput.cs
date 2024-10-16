using Volo.Abp.Application.Dtos;

namespace VCardOnAbp.Payments.Dtos;
public class GetPaymentMethodsInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}
