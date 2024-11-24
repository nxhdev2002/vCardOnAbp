using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VCardOnAbp.Currencies.Dto;
using VCardOnAbp.Permissions;
using Volo.Abp.Domain.Repositories;

namespace VCardOnAbp.Currencies;

[Authorize(VCardOnAbpPermissions.CurrencyGroup)]
public class CurrencyAppService(
    IRepository<Currency, Guid> currencyRepository
) : VCardOnAbpAppService, ICurrencyAppService
{
    private readonly IRepository<Currency, Guid> _currencyRepository = currencyRepository;

    [Authorize(VCardOnAbpPermissions.AddCurrency)]
    public async Task CreateAsync(CreateCurrencyDto input)
    {
        Currency currency = new(
            GuidGenerator.Create(),
            input.Name,
            input.Code,
            input.Symbol,
            input.UsdRate.GetValueOrDefault(1)
        );
        await _currencyRepository.InsertAsync(currency);
    }

    [Authorize(VCardOnAbpPermissions.EditCurrency)]
    public async Task DeleteAsync(Guid id)
    {
        await _currencyRepository.DeleteAsync(id);
    }

    [Authorize(VCardOnAbpPermissions.ViewCurrency)]
    public async Task<CurrencyDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<Currency, CurrencyDto>(await _currencyRepository.GetAsync(id));
    }

    [Authorize(VCardOnAbpPermissions.ViewCurrency)]
    public async Task<List<CurrencyDto>> GetListAsync()
    {
        List<Currency> currencies = await _currencyRepository.GetListAsync();
        return ObjectMapper.Map<List<Currency>, List<CurrencyDto>>(currencies);
    }

    [Authorize(VCardOnAbpPermissions.EditCurrency)]
    public Task UpdateAsync(Guid id, UpdateCurrencyDto input)
    {
        Currency currency = new(
            id,
            input.Name,
            input.Code,
            input.Symbol,
            input.UsdRate.GetValueOrDefault(1)
        );
        return _currencyRepository.UpdateAsync(currency);
    }
}
