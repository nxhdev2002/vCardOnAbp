using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using VCardOnAbp.ApiServices.Vcc51;
using VCardOnAbp.ApiServices.Vcc51.Dtos;
using VCardOnAbp.BackgroundJobs.Dtos;
using VCardOnAbp.Cards;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;
using Volo.Abp.Uow;

namespace VCardOnAbp.BackgroundJobs;

public class FundCardJob(
    ICancellationTokenProvider cancellationTokenProvider,
    IVcc51AppService vcc51AppService,
    IUnitOfWorkManager unitOfWorkManager
)
    : AsyncBackgroundJob<FundCardJobArgs>, ITransientDependency
{
    private readonly ICancellationTokenProvider _cancellationTokenProvider = cancellationTokenProvider;
    private readonly IVcc51AppService _vcc51AppService = vcc51AppService;
    private readonly IUnitOfWorkManager _unitOfWorkManager = unitOfWorkManager;

    public override async Task ExecuteAsync(FundCardJobArgs args)
    {
        _cancellationTokenProvider.Token.ThrowIfCancellationRequested();
        await ProcessAsync(args);
    }

    private async Task ProcessAsync(FundCardJobArgs args)
    {
        using IUnitOfWork uow = _unitOfWorkManager.Begin(requiresNew: true, isTransactional: true);
        Logger.LogInformation($"{nameof(FundCardJob)}: {args.CardId}");
        switch (args.Supplier)
        {
            case Supplier.Vmcardio:
                return;
            case Supplier.Vcc51:
                await _vcc51AppService.FundingCard(new Vcc51FundCardInput(
                    args.CardId, args.Amount
                ));
                break;
            default:
                return;
        }
        await uow.CompleteAsync();
    }
}
