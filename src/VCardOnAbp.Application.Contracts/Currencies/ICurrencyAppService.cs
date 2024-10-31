using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VCardOnAbp.Currencies.Dto;
using Volo.Abp.Application.Services;

namespace VCardOnAbp.Currencies;
public interface ICurrencyAppService : IApplicationService
{
    Task<CurrencyDto> GetAsync(Guid id);
    Task<List<CurrencyDto>> GetListAsync();
    Task CreateAsync(CreateCurrencyDto input);
    Task UpdateAsync(Guid id, UpdateCurrencyDto input);
    Task DeleteAsync(Guid id);
}
