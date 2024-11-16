using System.Collections.Generic;
using System.Threading.Tasks;
using VCardOnAbp.ApiServices.Vcc51.Dtos;
using Volo.Abp.Application.Services;

namespace VCardOnAbp.ApiServices.Vcc51;
public interface IVcc51AppService : IApplicationService
{
    Task<List<Vcc51CardTransactionDto>> GetTransaction(string cardNo);
    Task<Vcc51Card> GetCardInfo(string cardNo);
}
