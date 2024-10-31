using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VCardOnAbp.Currencies.Dto;
using Volo.Abp.Domain.Repositories;

namespace VCardOnAbp.Currencies;
public class CurrencyAppService(
    IRepository<Currency, Guid> currencyRepository    
) : VCardOnAbpAppService, ICurrencyAppService
{
    private readonly IRepository<Currency, Guid> _currencyRepository = currencyRepository;

    public async Task CreateAsync(CreateCurrencyDto input)
    {
        var currency = new Currency(
            GuidGenerator.Create(),
            input.Name,
            input.Code,
            input.Symbol,
            input.UsdRate.GetValueOrDefault(1)
        );
        await _currencyRepository.InsertAsync(currency);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _currencyRepository.DeleteAsync(id);
    }

    public async Task<CurrencyDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<Currency, CurrencyDto>(await _currencyRepository.GetAsync(id));
    }

    public async Task<List<CurrencyDto>> GetListAsync()
    {
        var currencies = await _currencyRepository.GetListAsync();
        return ObjectMapper.Map<List<Currency>, List<CurrencyDto>>(currencies);
    }

    public Task UpdateAsync(Guid id, UpdateCurrencyDto input)
    {
        var currency = new Currency(
            id,
            input.Name,
            input.Code,
            input.Symbol,
            input.UsdRate.GetValueOrDefault(1)
        );
        return _currencyRepository.UpdateAsync(currency);
    }
}
