using Volo.Abp.Application.Dtos;

namespace VCardOnAbp.Cards.Dto;

public class GetCardTransactionInput : PagedResultRequestDto
{
    public string? Filter { get; set; }
}
