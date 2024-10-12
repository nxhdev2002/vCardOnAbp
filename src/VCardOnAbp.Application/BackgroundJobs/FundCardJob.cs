using System.Threading.Tasks;
using VCardOnAbp.ApiServices.Vmcardio;
using VCardOnAbp.BackgroundJobs.Dtos;
using VCardOnAbp.Cards;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;

namespace VCardOnAbp.BackgroundJobs;

public class FundCardJob(
    ICancellationTokenProvider cancellationTokenProvider,
    IVmcardioAppService vmcardioAppService
)
    : AsyncBackgroundJob<FundCardJobArgs>, ITransientDependency
{
    private readonly ICancellationTokenProvider _cancellationTokenProvider = cancellationTokenProvider;
    private readonly IVmcardioAppService _vmcardioAppService = vmcardioAppService;

    public override async Task ExecuteAsync(FundCardJobArgs args)
    {
        _cancellationTokenProvider.Token.ThrowIfCancellationRequested();
        await ProcessAsync(args);
    }

    private async Task ProcessAsync(FundCardJobArgs args)
    {
        switch (args.Supplier)
        {
            case Supplier.Vmcardio:
                await _vmcardioAppService.FundCardAsync(new ApiServices.Vmcardio.Dtos.FundCardDto());
                break;
            default:
                return;
        }
    }
}
