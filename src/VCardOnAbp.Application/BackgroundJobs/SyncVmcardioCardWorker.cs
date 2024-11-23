using Hangfire;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using VCardOnAbp.ApiServices.Vmcardio;
using VCardOnAbp.ApiServices.Vmcardio.Dtos;
using VCardOnAbp.Cards;
using VCardOnAbp.Masters;
using Volo.Abp.BackgroundWorkers.Hangfire;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace VCardOnAbp.BackgroundJobs;
public class SyncVmcardioCardWorker : HangfireBackgroundWorkerBase
{
    private readonly ICardRepository _cardRepository;
    private readonly IVmcardioAppService _vmcardioAppService;
    private readonly IRepository<Bin, Guid> _binRepository; 

    public SyncVmcardioCardWorker(
        ICardRepository cardRepository,
        IVmcardioAppService vmcardioAppService,
        IRepository<Bin, Guid> binRepository
    )
    {
        RecurringJobId = nameof(SyncVmcardioCardWorker);
        CronExpression = Cron.MinuteInterval(5);

        _cardRepository = cardRepository;
        _vmcardioAppService = vmcardioAppService;
        _binRepository = binRepository;
    }

    public override async Task DoWorkAsync(CancellationToken cancellationToken = default)
    {
        return;
        using IUnitOfWork uow = LazyServiceProvider.LazyGetRequiredService<IUnitOfWorkManager>().Begin();

        var bins = await (await _binRepository.GetQueryableAsync()).AsNoTracking().ToListAsync(cancellationToken);
        var cards = await _cardRepository.GetActiveCardAsync(Supplier.Vmcardio, token: cancellationToken);
        var result = cards.Join(
            bins,
            card => card.BinId,
            bin => bin.Id,
            (card, bin) => new
            {
                card,
                bin
            }
        );

        foreach (var item in result)
        {
            var supplierKey = JsonSerializer.Deserialize<VmcardioIdentifyDto>(item.card.SupplierIdentity);
            var vmCard = await _vmcardioAppService.GetCard(new GetVmcardioCardInput(item.bin.BinMapping, supplierKey!.card_id, supplierKey.uid));
            item.card.SetBalance(vmCard.available_amount.GetValueOrDefault(0) - item.card.Balance);
        }
    }
}
