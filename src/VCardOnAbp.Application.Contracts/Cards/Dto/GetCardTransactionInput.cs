using System;
using Volo.Abp.Application.Dtos;

namespace VCardOnAbp.Cards.Dto;

public class GetCardTransactionInput : PagedResultRequestDto
{
    public Guid CardId { get; set; }
    public string? Filter { get; set; }
}
