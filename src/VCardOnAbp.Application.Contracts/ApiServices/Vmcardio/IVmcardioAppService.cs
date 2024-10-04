using System.Threading.Tasks;
using VCardOnAbp.ApiServices.Vmcardio.Dtos;
using Volo.Abp.Application.Services;

namespace VCardOnAbp.ApiServices.Vmcardio
{
    public interface IVmcardioAppService : IApplicationService
    {
        Task<object> GetCards(GetCardsFilterInput input);
        Task<VmCardDto> GetCard(GetCardInput input);
        Task CreateCardAsync();
        Task DeleteCardAsync();
    }
}
