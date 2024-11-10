using AutoMapper;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VCardOnAbp.ApiServices.Vmcardio;
using VCardOnAbp.ApiServices.Vmcardio.Dtos;
using VCardOnAbp.Cards;
using Volo.Abp.BackgroundWorkers.Hangfire;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace VCardOnAbp.BackgroundJobs;

public class SyncVmcardioTransactionWorker : HangfireBackgroundWorkerBase
{
    private const string GetVmcardioTransactionType = "get_card_transactions";

    private readonly IVmcardioAppService _vmcardioAppService;
    private readonly ICardRepository _cardRepository;
    private readonly IRepository<CardTransaction, Guid> _cardTransactionRepository;
    private readonly IMapper _mapper;
    public SyncVmcardioTransactionWorker(
        IVmcardioAppService vmcardioAppService,
        ICardRepository cardRepository,
        IRepository<CardTransaction, Guid> cardTransactionRepository,
        IMapper mapper
    )
    {
        RecurringJobId = nameof(SyncVmcardioTransactionWorker);
        CronExpression = Cron.Hourly(2);

        _vmcardioAppService = vmcardioAppService;
        _cardRepository = cardRepository;
        _cardTransactionRepository = cardTransactionRepository;
        _mapper = mapper;
    }

    public override async Task DoWorkAsync(CancellationToken cancellationToken = default)
    {
        using IUnitOfWork uow = LazyServiceProvider.LazyGetRequiredService<IUnitOfWorkManager>().Begin();
        System.Collections.Generic.List<Card> cards = await _cardRepository.GetActiveCardAsync(Supplier.Vmcardio);
        foreach (Card card in cards)
        {
            System.Collections.Generic.List<VmCardioTransactionDto> trans = await _vmcardioAppService.GetCardTransactions(new GetVmCardTransactionInput
            {
                start = string.Empty,
                end = string.Empty,
                status = string.Empty,
                transaction_type = string.Empty,
                start_time = string.Empty,
                end_time = string.Empty,
                type = GetVmcardioTransactionType,
                card_id = card.SupplierIdentity,
                page = "1",
                page_size = "100",
                bin = "sun539502",
                card_number = card.CardNo,
                uid = "17552",
            });

            // filter exist transactions
            System.Collections.Generic.List<CardTransaction> cardTrans = await (await _cardTransactionRepository.GetQueryableAsync())
                .AsNoTracking()
                .Where(x => x.CardId == card.Id)
                .ToListAsync(cancellationToken);

            System.Collections.Generic.List<CardTransaction> transNotInDb = trans
                .Where(x => !cardTrans.Exists(y => x.AuthId == y.SupplierTranId))
                .Select(x => new CardTransaction(
                    card.Id,
                    x.AuthAmount.GetValueOrDefault(0),
                    x.AuthCurrency,
                    x.Description,
                    x.MerchantName,
                    x.SettleAmount.GetValueOrDefault(0),
                    x.Status,
                    x.AuthId
                ))
                .ToList();

            await _cardTransactionRepository.InsertManyAsync(transNotInDb, cancellationToken: cancellationToken);
        }
        await uow.CompleteAsync(cancellationToken);
    }
}
