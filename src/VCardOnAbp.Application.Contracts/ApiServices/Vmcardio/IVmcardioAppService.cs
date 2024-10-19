using System.Collections.Generic;
using System.Threading.Tasks;
using VCardOnAbp.ApiServices.Vmcardio.Dtos;
using Volo.Abp.Application.Services;

namespace VCardOnAbp.ApiServices.Vmcardio;

public interface IVmcardioAppService : IApplicationService
{
    Task<object> GetCards(GetCardsFilterInput input);
    Task<VmCardDto> GetCard(Dtos.GetCardInput input);
    Task<List<VmCardioTransactionDto>> GetCardTransactions(GetVmCardTransactionInput input);
    Task<object> CreateCardAsync(VmcardioCreateCardDto input);
    Task DeleteCardAsync();
    Task FundCardAsync(FundCardDto input);
}
