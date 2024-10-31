using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCardOnAbp.Bins.Dtos;
using VCardOnAbp.Currencies;
using VCardOnAbp.Masters;
using VCardOnAbp.Permissions;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;

namespace VCardOnAbp.Bins;

[Authorize(VCardOnAbpPermissions.BinGroup)]
public class BinCardAppService(
    IDistributedCache<Bin> distributedCache,
    IRepository<Bin, Guid> binRepository,
    IRepository<Currency, Guid> currencyRepository
) : VCardOnAbpAppService, IBinCardAppService
{
    private readonly IDistributedCache<Bin> _distributedCache = distributedCache;
    private readonly IRepository<Bin, Guid> _binRepository = binRepository;
    private readonly IRepository<Currency, Guid> _currencyRepository = currencyRepository;


    [Authorize(VCardOnAbpPermissions.AddBin)]
    public virtual async Task<BinDto> CreateAsync(CreateBinDtoInput input)
    {
        Currency currency = await _currencyRepository.GetAsync(input.CurrencyId);
        Bin bin = new(
            GuidGenerator.Create(),
            input.Name,
            input.Description,
            input.Supplier,
            currency.Id,
            input.SupplierMapping,
            input.CreationFixedFee,
            input.CreationPercentFee,
            input.FundingFixedFee,
            input.FundingPercentFee
        );

        await _binRepository.InsertAsync(bin);
        BinDto result = ObjectMapper.Map<Bin, BinDto>(bin);
        result.Currency = currency.Code;
        return result;
    }

    [Authorize(VCardOnAbpPermissions.ViewBin)]
    public virtual async Task<BinDto> GetAsync(Guid id)
    {
        Bin? bin = await _distributedCache
            .GetOrAddAsync(id.ToString(), async () => await _binRepository.GetAsync(id));
        return ObjectMapper.Map<Bin, BinDto>(bin);
    }

    [Authorize(VCardOnAbpPermissions.ViewBin)]
    public virtual async Task<List<BinDto>> GetListAsync(GetBinDtoInput input)
    {
        IQueryable<Bin> binQuery = (await _binRepository.GetQueryableAsync())
                        .AsNoTracking()
                        .PageBy(input)
                        .WhereIf(input.Filter != null,
                                x => EF.Functions.Like(x.Name, $"%{input.Filter}%") ||
                                     EF.Functions.Like(x.Description, $"%{input.Filter}%"));

        IQueryable<Currency> currencyQuery = await _currencyRepository.GetQueryableAsync();
        IQueryable<BinDto> query = from b in binQuery
                                   join c in currencyQuery on b.CurrencyId equals c.Id into bc
                                   from c in bc.DefaultIfEmpty()
                                   select new BinDto
                                   {
                                       Id = b.Id,
                                       Name = b.Name,
                                       Description = b.Description,
                                       Currency = c.Name,
                                       Symbol = c.Symbol,
                                       CreationFixedFee = b.CreationFixedFee,
                                       CreationPercentFee = b.CreationPercentFee,
                                       FundingFixedFee = b.FundingFixedFee,
                                       FundingPercentFee = b.FundingPercentFee,
                                   };

        List<BinDto> bin = await query
            .ToListAsync()
            .ConfigureAwait(false);

        return bin;
    }

    [Authorize(VCardOnAbpPermissions.EditBin)]
    public virtual async Task<BinDto> UpdateAsync(Guid id, UpdateBinDtoInput input)
    {
        Bin bin = await _binRepository.GetAsync(id);

        bin.If(input.FundingPercentFee > 0, b => b.UpdateFee(fundingPercentFee: input.FundingPercentFee));
        bin.If(input.FundingFixedFee > 0, b => b.UpdateFee(fundingFixedFee: input.FundingFixedFee));
        bin.If(input.CreationFixedFee > 0, b => b.UpdateFee(creationFixedFee: input.CreationFixedFee));
        bin.If(input.CreationPercentFee > 0, b => b.UpdateFee(creationPercentFee: input.CreationPercentFee));

        await _distributedCache.SetAsync(bin.Id.ToString(), bin, new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions
        {
            SlidingExpiration = System.TimeSpan.FromMinutes(30)
        });
        return ObjectMapper.Map<Bin, BinDto>(bin);
    }

    [Authorize(VCardOnAbpPermissions.EditBin)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _distributedCache.RemoveAsync(id.ToString());
        await _binRepository.DeleteAsync(id);
    }
}
