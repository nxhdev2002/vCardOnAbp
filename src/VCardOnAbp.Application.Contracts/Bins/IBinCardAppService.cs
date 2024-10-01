using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VCardOnAbp.Bins.Dtos;
using Volo.Abp.Application.Services;

namespace VCardOnAbp.Bins
{
    public interface IBinCardAppService : IApplicationService
    {
        Task<BinDto> GetAsync(Guid id);
        Task<List<BinDto>> GetListAsync(GetBinDtoInput input);
        Task<BinDto> CreateAsync(CreateBinDtoInput input);
        Task<BinDto> UpdateAsync(Guid id, UpdateBinDtoInput input);
        Task DeleteAsync(Guid id);
    }
}
