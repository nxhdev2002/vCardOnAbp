using System;
using Volo.Abp.Application.Dtos;

namespace VCardOnAbp.Cards.Dto;

public class FundCardInput : EntityDto<Guid>
{
    public decimal Amount { get; set; }
}
