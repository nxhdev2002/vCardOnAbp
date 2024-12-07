using Hangfire;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VCardOnAbp.ApiServices.Vcc51;
using VCardOnAbp.ApiServices.Vcc51.Dtos;
using VCardOnAbp.Cards;
using VCardOnAbp.Masters;
using Volo.Abp.BackgroundWorkers.Hangfire;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Uow;

namespace VCardOnAbp.BackgroundJobs.Vcc51;
public class SyncVcc51CardTransactionWorker : HangfireBackgroundWorkerBase
{
    private readonly ICardRepository _cardRepository;
    private readonly IVcc51AppService _vcc51AppService;
    private readonly IRepository<Bin, Guid> _binRepository;
    private readonly IRepository<CardTransaction, Guid> _cardTransRepo;
    private readonly IGuidGenerator _guidGenerator;
    public SyncVcc51CardTransactionWorker(
        ICardRepository cardRepository,
        IVcc51AppService vcc51AppService,
        IRepository<Bin, Guid> binRepository,
        IRepository<CardTransaction, Guid> cardTransRepo,
        IGuidGenerator guidGenerator
    )
    {
        RecurringJobId = nameof(SyncVcc51CardTransactionWorker);
        CronExpression = Cron.MinuteInterval(5);

        _cardRepository = cardRepository;
        _vcc51AppService = vcc51AppService;
        _binRepository = binRepository;
        _cardTransRepo = cardTransRepo;
        _guidGenerator = guidGenerator;
    }

    public async override Task DoWorkAsync(CancellationToken cancellationToken = default)
    {
        Logger.LogInformation($"{nameof(SyncVcc51CardTransactionWorker)}: Syncing Vcc51 card transaction begin");
        using IUnitOfWork uow = LazyServiceProvider.LazyGetRequiredService<IUnitOfWorkManager>().Begin();
        System.Collections.Generic.List<Card> cards = await _cardRepository.GetActiveCardAsync(Supplier.Vcc51, token: cancellationToken);
        foreach (Card card in cards)
        {
            try
            {
                ApiServices.Vcc51.Dtos.Vcc51Card vcc51Card = await _vcc51AppService.GetCardInfo(card.CardNo);
                if (string.IsNullOrEmpty(vcc51Card.CardNo)) throw new Exception();
                card.SetBalance(decimal.Parse(vcc51Card.Amount!.Replace(".", ",")) - card.Balance);
                card.SetSecret(null, vcc51Card.Cvv, vcc51Card.Exp?.Insert(2, "/"));
                if (vcc51Card.Status != Vcc51Const.CardActiveStatus) card.ChangeStatus(CardStatus.Lock);
                card.ChangeStatus(CardStatus.Active);

                card.LastSync = DateTime.UtcNow;

                await SyncTransaction(card, cancellationToken);

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(SyncVcc51CardTransactionWorker)}: Error syncing card {card.Id} transaction");

            }
        }
        await uow.CompleteAsync();
        Logger.LogInformation($"{nameof(SyncVcc51CardTransactionWorker)}: Syncing Vcc51 card transaction done");
    }

    private async Task SyncTransaction(Card card, CancellationToken cancellationToken)
    {
        Logger.LogInformation($"{nameof(SyncVcc51CardTransactionWorker)}: Syncing card {card.Id} transaction...");

        List<Vcc51CardTransactionDto> transactions = await _vcc51AppService.GetTransaction(card.CardNo);
        List<CardTransaction> dbTrans = transactions.Select(t =>
        {
            string[] amount = t.Amount.Split(' ');
            DateTime parsed;
            DateTime.TryParseExact(t.TransactionTime, "yyyy/MM/dd HH:mm:ss", CultureInfo.CurrentCulture, DateTimeStyles.None, out parsed);

            return new CardTransaction(
                _guidGenerator.Create(),
                card.Id,
                decimal.Parse(amount[0].Replace('.', ',')),
                amount[1],
                t.Description,
                t.Merchant,
                decimal.Parse(t.AuthorizationAmt.Replace('.', ',')),
                Vcc51Const.TransactionStatuses.GetValueOrDefault(t.Type, t.Type),
                t.TransactionTime,
                parsed
            );
        }
        ).ToList();

        List<CardTransaction> transaction = await _cardTransRepo.GetListAsync(t => t.CardId == card.Id, cancellationToken: cancellationToken);

        IEnumerable<CardTransaction> result = from vcc51 in dbTrans
                                            join db in transaction
                                            on vcc51.SupplierTranId equals db.SupplierTranId into res
                                            from r in res.DefaultIfEmpty()
                                            where r == null
                                            select vcc51;

        await _cardTransRepo.InsertManyAsync(result, true, cancellationToken);
        Logger.LogInformation($"{nameof(SyncVcc51CardTransactionWorker)}: Syncing card {card.Id} transaction done");
    }
}
