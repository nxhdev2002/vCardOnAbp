using System;
using Volo.Abp.Application.Dtos;

namespace VCardOnAbp.Cards.Dto;

public class CardDto : EntityDto<Guid>
{
    public string? CardNo { get; private set; }
    public decimal Balance { get; private set; }
    public CardStatus CardStatus { get; private set; }
    public string? CardName { get; private set; }
    public Guid? BinId { get; private set; }
}
