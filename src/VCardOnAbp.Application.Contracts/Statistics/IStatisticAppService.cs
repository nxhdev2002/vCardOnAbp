using System.Threading.Tasks;
using VCardOnAbp.Statistics.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace VCardOnAbp.Statistics;
public interface IStatisticAppService : IApplicationService
{
    Task<StatisticDto> GetAsync();
    Task<PagedResultDto<StatisticDto>> GetAllStatisticsAsync(PagedResultRequestDto input);
}
