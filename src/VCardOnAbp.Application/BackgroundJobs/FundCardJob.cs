using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using VCardOnAbp.ApiServices.Vmcardio;
using VCardOnAbp.ApiServices.Vmcardio.Dtos;
using VCardOnAbp.BackgroundJobs.Dtos;
using VCardOnAbp.Cards;
using VCardOnAbp.Masters;
using VCardOnAbp.Transactions;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Threading;

namespace VCardOnAbp.BackgroundJobs;

public class FundCardJob(
    ICancellationTokenProvider cancellationTokenProvider,
    IVmcardioAppService vmcardioAppService,
    ICardRepository cardRepository,
    IRepository<Bin, Guid> binRepository 
)
    : AsyncBackgroundJob<FundCardJobArgs>, ITransientDependency
{
    private readonly ICancellationTokenProvider _cancellationTokenProvider = cancellationTokenProvider;
    private readonly IVmcardioAppService _vmcardioAppService = vmcardioAppService;
    private readonly ICardRepository _cardRepository = cardRepository;
    private readonly IRepository<Bin, Guid> _binRepository = binRepository;

    public override async Task ExecuteAsync(FundCardJobArgs args)
    {
        _cancellationTokenProvider.Token.ThrowIfCancellationRequested();
        await ProcessAsync(args);
    }

    private async Task ProcessAsync(FundCardJobArgs args)
    {
        using var repo = _cardRepository.DisableTracking();
        var card = await _cardRepository.GetAsync(args.CardId);
        var supplierKey = JsonSerializer.Deserialize<VmcardioIdentifyDto>(card.SupplierIdentity);
        var bin = await _binRepository.GetAsync(card.BinId);

        var isSuccess = false;
        switch (args.Supplier)
        {
            case Supplier.Vmcardio:
                var result = await _vmcardioAppService.FundCardAsync(new VmcardioFundCardDto()
                {
                    amount = args.Amount.ToString(),
                    card_id = supplierKey?.card_id!,
                    card_number = card.CardNo,
                    uid = supplierKey?.uid!,
                    bin = bin.BinMapping!
                });

                isSuccess = result.code == StatusCodes.Status200OK;
                break;
            default:
                return;
        }
    }
}
