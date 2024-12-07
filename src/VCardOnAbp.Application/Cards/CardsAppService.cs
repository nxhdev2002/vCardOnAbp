using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCardOnAbp.BackgroundJobs.Dtos;
using VCardOnAbp.Cards.Dto;
using VCardOnAbp.Models;
using VCardOnAbp.Permissions;
using VCardOnAbp.Security;
using VCardOnAbp.Transactions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;

namespace VCardOnAbp.Cards;

[Authorize(VCardOnAbpPermissions.CardGroup)]
public class CardsAppService(
    CardManager cardManager,
    IBackgroundJobManager backgroundJobManager,
    ICardRepository cardRepository,
    IRepository<CardTransaction, Guid> cardTransactionRepository,
    IRepository<UserTransaction, Guid> userTransactionRepository,
    IDistributedCache<string> distributedCache,
    IAuthorizationService authorizationService
) : VCardOnAbpAppService, ICardsAppService
{
    private readonly CardManager _cardManager = cardManager;
    private readonly ICardRepository _cardRepository = cardRepository;
    private readonly IRepository<CardTransaction, Guid> _cardTransactionRepository = cardTransactionRepository;
    private readonly IBackgroundJobManager _backgroundJobManager = backgroundJobManager;
    private readonly IRepository<UserTransaction, Guid> _userTransaction = userTransactionRepository;
    private readonly IDistributedCache<string> _distributedCache = distributedCache;
    private readonly IAuthorizationService _authorizationService = authorizationService;

    private const string DistributedLockValue = "1";

    [Authorize(VCardOnAbpPermissions.ViewCard)]
    public virtual async Task<PagedResultDto<CardDto>> GetListAsync(GetCardInput input)
    {
        IQueryable<Card> cards = (await _cardRepository.GetQueryableAsync())
            .AsNoTracking()
            .Where(x => x.OwnerId == CurrentUser.Id!.Value)
            .WhereIf(!string.IsNullOrEmpty(input.Filter), x => EF.Functions.Like(x.CardNo, $"%{input.Filter}%"));

        int totalCount = await cards.CountAsync();
        List<Card> data = await cards
            .PageBy(input)
            .ToListAsync();

        return new PagedResultDto<CardDto>(
            totalCount,
            ObjectMapper.Map<List<Card>, List<CardDto>>(data)
        );
    }


    [Authorize(VCardOnAbpPermissions.ViewCard)]
    public virtual async Task<CardDto> GetAsync(Guid id)
    {
        Card? card = await _cardManager.GetCard(id, CurrentUser.Id!.Value);
        return card == null ?
            throw new UserFriendlyException(L["CardNotFound"]) : ObjectMapper.Map<Card, CardDto>(card);
    }


    [Authorize(VCardOnAbpPermissions.ViewCardTransaction)]
    public virtual async Task<PagedResultDto<CardTransactionDto>> GetTransactionAsync(Guid id, GetCardTransactionInput input)
    {
        Card card = await _cardManager.GetCard(id, CurrentUser.Id!.Value, true);

        IQueryable<CardTransaction> transaction = (await _cardTransactionRepository.GetQueryableAsync())
            .AsNoTracking()
            .Where(x => x.CardId == id)
            .WhereIf(!string.IsNullOrEmpty(input.Filter), x => EF.Functions.Like(x.Description, $"%{input.Filter}%"));

        int totalCount = await transaction.CountAsync();
        List<CardTransaction> data = await transaction
            .PageBy(input)
            .ToListAsync();

        return new PagedResultDto<CardTransactionDto>(
            totalCount,
            ObjectMapper.Map<List<CardTransaction>, List<CardTransactionDto>>(data)
        );
    }


    [Authorize(VCardOnAbpPermissions.CreateCard)]
    public virtual async Task<ResponseModel> CreateAsync(CreateCardInput input)
    {
        var distributedLockKey = $"{CurrentUser.Id}:CreateCard";
        if (await _distributedCache.GetAsync(distributedLockKey) != null)
        {
            return ResponseModel.ErrorResponse(L["CardCreationInProgress"]);
        }

        input.SanitizeInput();

        Card card;
        try
        {
            await _distributedCache.SetAsync(distributedLockKey, DistributedLockValue,
                new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(10)
                }
            );
            card = await _cardManager.CreateCard("_", input.BinId, string.Empty, input.CardName, CardStatus.Pending, input.Amount, CurrentUser.Id!.Value, input.Note)
            ?? throw new UserFriendlyException(L["CardCreationFailed"]);

            await _cardRepository.InsertAsync(card);
        }
        finally
        {
            // remove lock
            await _distributedCache.RemoveAsync(distributedLockKey);
        }

        await _backgroundJobManager.EnqueueAsync(new CreateCardJobArgs
        {
            CardId = card.Id,
            CardName = card.CardName,
            Supplier = card.Supplier,
            UserId = CurrentUser.Id!.Value,
            Amount = input.Amount,
            BinId = input.BinId,
        });

        return ResponseModel.SuccessResponse(L["SuccessToast", L["Card:CreateCard"]]);
    }


    [Authorize(VCardOnAbpPermissions.FundCard)]
    public virtual async Task<ResponseModel> FundAsync(Guid id, FundCardInput input)
    {
        var distributedLockKey = $"{CurrentUser.Id}:FundCard";
        if (await _distributedCache.GetAsync(distributedLockKey) != null)
        {
            return ResponseModel.ErrorResponse(L["CardFundingInProgress"]);
        }

        Card card;
        try
        {
            await _distributedCache.SetAsync(distributedLockKey, DistributedLockValue,
                new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(10)
                }
            );
            card = await _cardManager.GetCard(id, CurrentUser.Id!.Value)
            ?? throw new UserFriendlyException(L["CardNotFound"]);

            await _cardManager.FundCard(card, input.Amount);
        }
        finally
        {
            await _distributedCache.RemoveAsync(distributedLockKey);
        }

        // enqueue fund job
        await _backgroundJobManager.EnqueueAsync(new FundCardJobArgs
        {
            Supplier = card.Supplier,
            CardId = card.Id,
            UserId = card.CreatorId!.Value,
            Amount = input.Amount
        });

        await _userTransaction.InsertAsync(
            new UserTransaction(GuidGenerator.Create(), card.CreatorId!.Value, card.Id, L["CardPermission:Fund"], UserTransactionType.FundCard, input.Amount)
        );

        return ResponseModel.SuccessResponse(L["SuccessToast", L["CardPermission:Fund"]]);
    }


    [Authorize(VCardOnAbpPermissions.DeleteCard)]
    public virtual async Task DeleteAsync(Guid id)
    {
        Card card = await _cardManager.GetCard(id, CurrentUser.Id!.Value) ?? throw new UserFriendlyException(L["CardNotFound"]);
        await _cardManager.DeleteAsync(card!);
    }


    [Authorize(VCardOnAbpPermissions.ViewCard)]
    public virtual async Task<CardSecretDto> GetSecretAsync(Guid id)
    {
        (string cvv, string exp) = await _cardManager.ViewCardSecret(id, CurrentUser.Id!.Value);
        return new CardSecretDto(cvv, exp);
    }



    [RemoteService(false)]
    public async Task BuildCardRowActions(CardDto card)
    {
        card.RowActions.Add(CardRowAction.Note);
        if (await _authorizationService.IsGrantedAsync(VCardOnAbpPermissions.Manager))
        {
            if (card.CardStatus == CardStatus.PendingDelete)
            {
                card.RowActions.Add(CardRowAction.ApproveDelete);
                card.RowActions.Add(CardRowAction.RejectDelete);
            }
        }

        if (card.CardStatus == CardStatus.Active)
        {
            card.RowActions.Add(CardRowAction.Refresh);
            card.RowActions.Add(CardRowAction.Fund);
            card.RowActions.Add(CardRowAction.Delete);
            card.RowActions.Add(CardRowAction.View);
        }
    }

}
