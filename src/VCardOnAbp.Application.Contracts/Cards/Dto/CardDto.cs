using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace VCardOnAbp.Cards.Dto;

public class CardDto : EntityDto<Guid>
{
    public string? CardNo { get; set; }
    public decimal Balance { get; set; }
    public CardStatus CardStatus { get; set; }
    public string? CardName { get; set; }
    public Guid? BinId { get; set; }
    public string? Note { get; set; }
    public List<CardRowAction> RowActions { get; set; } = [];
}
