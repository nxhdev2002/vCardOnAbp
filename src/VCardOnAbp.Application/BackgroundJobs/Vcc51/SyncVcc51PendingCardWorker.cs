using Hangfire;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Threading;
using VCardOnAbp.ApiServices.Vcc51;
using VCardOnAbp.Cards;
using Volo.Abp.BackgroundWorkers.Hangfire;
using Volo.Abp.Uow;
using System.Linq;

namespace VCardOnAbp.BackgroundJobs.Vcc51;
public class SyncVcc51PendingCardWorker : HangfireBackgroundWorkerBase
{
    private readonly ICardRepository _cardRepository;
    private readonly IVcc51AppService _vcc51AppService;
    private const int PAGESIZE = 100;
    public SyncVcc51PendingCardWorker(
        ICardRepository cardRepository,
        IVcc51AppService vcc51AppService
    )
    {
        RecurringJobId = nameof(SyncVcc51PendingCardWorker);
        CronExpression = Cron.Minutely();

        _cardRepository = cardRepository;
        _vcc51AppService = vcc51AppService;
    }

    public async override Task DoWorkAsync(CancellationToken cancellationToken = default)
        {
        Logger.LogInformation($"{nameof(SyncVcc51PendingCardWorker)}: Syncing Vcc51 card transaction begin");
        using IUnitOfWork uow = LazyServiceProvider.LazyGetRequiredService<IUnitOfWorkManager>().Begin();
        var cards = await _cardRepository.GetPendingCardAsync(Supplier.Vcc51, token: cancellationToken);
        foreach (var card in cards)
        {
            Logger.LogInformation($"{nameof(SyncVcc51PendingCardWorker)}: Syncing card {card.Id} transaction...");

            try
            {
                var vcc51Cards = await _vcc51AppService.GetCards(PAGESIZE);
                var vcc51Card = vcc51Cards.FirstOrDefault(x => x.Remark == card.SupplierIdentity && x.CardNo != null);
                if (vcc51Card == null) continue;

                card.SetSyncInfo(vcc51Card.CardNo!, vcc51Card.Cvv!, vcc51Card.Exp!.Insert(2, "/")); 
                card.ChangeStatus(CardStatus.Active);
                Logger.LogInformation($"{nameof(SyncVcc51PendingCardWorker)}: Syncing card {card.Id} transaction done");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(SyncVcc51PendingCardWorker)}: Error syncing card {card.Id} transaction");

            }
        }
        await uow.CompleteAsync();
        Logger.LogInformation($"{nameof(SyncVcc51PendingCardWorker)}: Syncing Vcc51 card transaction done");
    }
}
