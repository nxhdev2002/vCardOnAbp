using System.Threading.Tasks;
using VCardOnAbp.ApiServices.Vmcardio;
using VCardOnAbp.BackgroundJobs.Dtos;
using VCardOnAbp.Cards;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;

namespace VCardOnAbp.BackgroundJobs
{
    public class CreateCardJob(
        ICancellationTokenProvider cancellationTokenProvider,
        IVmcardioAppService vmcardioAppService
    )
        : AsyncBackgroundJob<CreateCardJobArgs>, ITransientDependency
    {
        private readonly ICancellationTokenProvider _cancellationTokenProvider = cancellationTokenProvider;
        private readonly IVmcardioAppService _vmcardioAppService = vmcardioAppService;

        public override async Task ExecuteAsync(CreateCardJobArgs args)
        {
            _cancellationTokenProvider.Token.ThrowIfCancellationRequested();
            await ProcessAsync(args);
        }

        private async Task ProcessAsync(CreateCardJobArgs args)
        {
            switch (args.Supplier)
            {
                case Supplier.Vmcardio:
                    await _vmcardioAppService.CreateCardAsync();
                    break;
                default:
                    return;
            }
        }
    }
}
