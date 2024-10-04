using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCardOnAbp.Bins.Dtos;
using VCardOnAbp.Masters;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;

namespace VCardOnAbp.Bins
{
    public class BinCardAppService(
        IDistributedCache<Bin> distributedCache,
        IRepository<Bin, Guid> binRepository
    ) : VCardOnAbpAppService, IBinCardAppService
    {
        private readonly IDistributedCache<Bin> _distributedCache = distributedCache;
        private readonly IRepository<Bin, Guid> _binRepository = binRepository;



        public virtual async Task<BinDto> CreateAsync(CreateBinDtoInput input)
        {
            var bin = new Bin(
                GuidGenerator.Create(),
                input.Name,
                input.Description,
                input.Supplier
            );

            await _binRepository.InsertAsync(bin);

            return ObjectMapper.Map<Bin, BinDto>(bin);
        }

        public virtual async Task<BinDto> GetAsync(Guid id)
        {
            var bin = await _distributedCache
                .GetOrAddAsync(id.ToString(), async () => await _binRepository.GetAsync(id));
            return ObjectMapper.Map<Bin, BinDto>(bin);
        }

        public virtual async Task<List<BinDto>> GetListAsync(GetBinDtoInput input)
        {
            var bin = await (await _binRepository.GetQueryableAsync())
                .AsNoTracking()
                .PageBy(input)
                .WhereIf(input.Filter != null,
                        x => EF.Functions.Like(x.Name, $"%{input.Filter}%") ||
                             EF.Functions.Like(x.Description, $"%{input.Filter}%"))
                .ToListAsync()
                .ConfigureAwait(false);

            return ObjectMapper.Map<List<Bin>, List<BinDto>>(bin);
        }

        public virtual async Task<BinDto> UpdateAsync(Guid id, UpdateBinDtoInput input)
        {
            var bin = await _binRepository.GetAsync(id);

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

        public virtual async Task DeleteAsync(Guid id)
        {
            await _distributedCache.RemoveAsync(id.ToString());
            await _binRepository.DeleteAsync(id);
        }
    }
}
