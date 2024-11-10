using System.Threading.Tasks;
using VCardOnAbp.BackgroundJobs.Dtos;
using VCardOnAbp.Cards.Events;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace VCardOnAbp.Cards.EventHandlers;

public class CardFundedEventHandler(
    IBackgroundJobManager backgroundJobManager
) : ILocalEventHandler<CardFundedEvent>, ITransientDependency
{
    private readonly IBackgroundJobManager _backgroundJobManager = backgroundJobManager;

    public async Task HandleEventAsync(CardFundedEvent eventData)
    {
        await _backgroundJobManager.EnqueueAsync(new FundCardJobArgs
        {
            Supplier = eventData.Card.Supplier,
            CardId = eventData.Card.Id,
            UserId = eventData.Card.CreatorId!.Value,
            Amount = eventData.Amount
        });
    }
}
