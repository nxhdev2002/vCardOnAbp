﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCardOnAbp.BackgroundJobs.Dtos;
using VCardOnAbp.Cards.Dto;
using VCardOnAbp.Cards.Events;
using VCardOnAbp.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Local;

namespace VCardOnAbp.Cards;

[Authorize(VCardOnAbpPermissions.CardGroup)]
public class CardsAppService(
    CardManager cardManager,
    IBackgroundJobManager backgroundJobManager,
    ICardRepository cardRepository,
    IRepository<CardTransaction, Guid> cardTransactionRepository,
    ILocalEventBus localEventBus
) : VCardOnAbpAppService, ICardsAppService
{
    private readonly CardManager _cardManager = cardManager;
    private readonly ICardRepository _cardRepository = cardRepository;
    private readonly IRepository<CardTransaction, Guid> _cardTransactionRepository = cardTransactionRepository;
    private readonly IBackgroundJobManager _backgroundJobManager = backgroundJobManager;
    private readonly ILocalEventBus _localEventBus = localEventBus;

    [Authorize(VCardOnAbpPermissions.ViewCard)]
    public virtual async Task<PagedResultDto<CardDto>> GetListAsync(GetCardInput input)
    {
        IQueryable<Card> cards = (await _cardRepository.GetQueryableAsync())
            .AsNoTracking()
            .Where(x => x.CreatorId == CurrentUser.Id!.Value)
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
    public virtual async Task<PagedResultDto<CardTransactionDto>> GetTransactionAsync(GetCardTransactionInput input)
    {
        using (_cardRepository.DisableTracking())
        {
            Card card = await _cardManager.GetCard(input.CardId, CurrentUser.Id!.Value)
                ?? throw new UserFriendlyException(L["CardNotFound"]);

            using (_cardTransactionRepository.DisableTracking())
            {
                IQueryable<CardTransaction> transaction = (await _cardTransactionRepository.GetQueryableAsync())
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
        }
    }


    [Authorize(VCardOnAbpPermissions.CreateCard)]
    public virtual async Task CreateAsync(CreateCardInput input)
    {
        await _backgroundJobManager.EnqueueAsync(new CreateCardJobArgs
        {
            CardName = input.CardName,
            Supplier = input.Supplier,
            UserId = CurrentUser.Id!.Value,
            Amount = input.Amount
        });
    }


    [Authorize(VCardOnAbpPermissions.FundCard)]
    public virtual async Task FundAsync(FundCardInput input)
    {
        Card card = await _cardManager.GetCard(input.Id, CurrentUser.Id!.Value)
            ?? throw new UserFriendlyException(L["CardNotFound"]);

        await _cardManager.FundCard(card, input.Amount);

        // publish event
        await _localEventBus.PublishAsync(new CardFundedEvent(card, input.Amount));
    }


    [Authorize(VCardOnAbpPermissions.DeleteCard)]
    public virtual async Task DeleteAsync(Guid id)
    {
        var card = await _cardManager.GetCard(id, CurrentUser.Id!.Value);
        await _cardManager.DeleteAsync(card!);
    }

    #region Admin Methods
    [Authorize(VCardOnAbpPermissions.AddCard)]
    public Task AddCard(AddCardInput input)
    {
        Card card = ObjectMapper.Map<AddCardInput, Card>(input);
        return _cardRepository.InsertAsync(card);
    }
    #endregion
}
