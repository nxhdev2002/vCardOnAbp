using System.Threading.Tasks;
using VCardOnAbp.Cards.Events;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace VCardOnAbp.Cards.EventHandlers;
public class CardStatusChangedEventHandler : ILocalEventHandler<CardStatusChangedEvent>, ITransientDependency
{
    public Task HandleEventAsync(CardStatusChangedEvent eventData)
    {
        throw new System.NotImplementedException();
    }
}
