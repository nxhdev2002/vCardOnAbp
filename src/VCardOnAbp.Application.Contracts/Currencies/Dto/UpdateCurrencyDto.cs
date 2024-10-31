using System;
using Volo.Abp.Application.Dtos;

namespace VCardOnAbp.Currencies.Dto;
public class UpdateCurrencyDto : CreateCurrencyDto, IEntityDto<Guid>
{
    public Guid Id { get; set; }
}
