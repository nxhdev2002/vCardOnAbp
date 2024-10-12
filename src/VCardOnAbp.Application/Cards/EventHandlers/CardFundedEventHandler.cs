using System;
using System.Threading.Tasks;
using VCardOnAbp.BackgroundJobs.Dtos;
using VCardOnAbp.Transactions;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus;
using Volo.Abp.Users;

namespace VCardOnAbp.Cards.EventHandlers
{
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
                UserId = eventData.Card.CreatorId!.Value,
                Amount = eventData.Amount
            });
        }
    }
}
