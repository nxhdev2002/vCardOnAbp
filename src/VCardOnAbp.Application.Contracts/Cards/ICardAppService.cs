using System.Threading.Tasks;
using VCardOnAbp.Cards.Dto;
using Volo.Abp.Application.Services;

namespace VCardOnAbp.Cards
{
    public interface ICardAppService : IApplicationService
    {
        Task Create(CreateCardInput input);
        Task<object> Action(ActionInput action);
    }
}
