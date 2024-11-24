using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using VCardOnAbp.ApiServices.Vcc51;
using VCardOnAbp.ApiServices.Vcc51.Dtos;
using VCardOnAbp.BackgroundJobs.Dtos;
using VCardOnAbp.Cards;
using VCardOnAbp.Masters;
using VCardOnAbp.Transactions;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Threading;
using Volo.Abp.Uow;

namespace VCardOnAbp.BackgroundJobs;

public class CreateCardJob(
    ICancellationTokenProvider cancellationTokenProvider,
    IVcc51AppService vcc51AppService,
    IRepository<Bin, Guid> binAppService,
    IRepository<UserTransaction, Guid> userTransaction,

    IUnitOfWorkManager unitOfWorkManager
)
    : AsyncBackgroundJob<CreateCardJobArgs>, ITransientDependency
{
    private readonly ICancellationTokenProvider _cancellationTokenProvider = cancellationTokenProvider;
    private readonly IVcc51AppService _vcc51AppService = vcc51AppService;
    private readonly IRepository<Bin, Guid> _binAppService = binAppService;
    private readonly IRepository<UserTransaction, Guid> _userTransaction = userTransaction;
    private readonly IUnitOfWorkManager _unitOfWorkManager = unitOfWorkManager;

    public override async Task ExecuteAsync(CreateCardJobArgs args)
    {
        _cancellationTokenProvider.Token.ThrowIfCancellationRequested();
        await ProcessAsync(args);
    }

    [UnitOfWork]
    private async Task ProcessAsync(CreateCardJobArgs args)
    {
        using (var uow = _unitOfWorkManager.Begin(requiresNew: true, isTransactional: true))
        {
            Logger.LogInformation($"{nameof(CreateCardJob)}: {args.CardId}");
            var bin = await (await _binAppService.GetQueryableAsync()).AsNoTracking().FirstOrDefaultAsync(x => x.Id == args.BinId);
            if (bin == null) return;

            var transaction = await (await _userTransaction.GetQueryableAsync()).AsNoTracking().FirstOrDefaultAsync(x => x.RelatedEntity == args.CardId);

            switch (args.Supplier)
            {
                case Supplier.Vmcardio:
                    return;
                case Supplier.Vcc51:
                    await _vcc51AppService.CreateCard(new Vcc51CreateCardInput(
                        bin.BinMapping, args.Amount, args.CardName, args.Amount + (transaction != null ? transaction.Amount : 0), args.CardId
                    ));
                    break;
                default:
                    return;
            }

            await uow.CompleteAsync();
        }
    }
}
