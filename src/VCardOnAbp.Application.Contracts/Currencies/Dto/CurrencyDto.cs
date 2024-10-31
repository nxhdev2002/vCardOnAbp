using System;
using Volo.Abp.Application.Dtos;

namespace VCardOnAbp.Currencies.Dto;
public class CurrencyDto : EntityDto<Guid>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Symbol { get; set; }
    public decimal? UsdRate { get; set; }
}
