using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCardOnAbp.BackgroundJobs.Dtos;
using VCardOnAbp.Cards.Dto;
using VCardOnAbp.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain.Repositories;

namespace VCardOnAbp.Cards
{
    [Authorize(VCardOnAbpPermissions.ViewCard)]
    public class CardsAppService(
        CardManager cardManager,
        IBackgroundJobManager backgroundJobManager,
        ICardRepository cardRepository,
        IRepository<CardTransaction, Guid> cardTransactionRepository
    ) : VCardOnAbpAppService, ICardsAppService
    {
        private readonly CardManager _cardManager = cardManager;
        private readonly ICardRepository _cardRepository = cardRepository;
        private readonly IRepository<CardTransaction, Guid> _cardTransactionRepository = cardTransactionRepository;
        private readonly IBackgroundJobManager _backgroundJobManager = backgroundJobManager;

        [Authorize(VCardOnAbpPermissions.ViewCard)]
        public virtual async Task<PagedResultDto<CardDto>> GetListAsync(GetCardInput input)
        {
            using (_cardRepository.DisableTracking())
            {
                var cards = (await _cardRepository.GetQueryableAsync())
                    .Where(x => x.CreatorId == CurrentUser.Id!.Value)
                    .WhereIf(!string.IsNullOrEmpty(input.Filter), x => EF.Functions.Like(x.CardNo, $"%{input.Filter}%"));
                
                var totalCount = await cards.CountAsync();
                var data = await cards
                    .PageBy(input)
                    .ToListAsync();

                return new PagedResultDto<CardDto>(
                    totalCount,
                    ObjectMapper.Map<List<Card>, List<CardDto>>(data)
                );
            }
        }


        [Authorize(VCardOnAbpPermissions.ViewCard)]
        public virtual async Task<CardDto> GetAsync(Guid id)
        {
            var card = await _cardManager.GetCard(id, CurrentUser.Id!.Value);
            return card == null ? 
                throw new UserFriendlyException(L["CardNotFound"]) : ObjectMapper.Map<Card, CardDto>(card);
        }


        [Authorize(VCardOnAbpPermissions.ViewCardTransaction)]
        public virtual async Task<PagedResultDto<CardTransactionDto>> GetTransactionAsync(GetCardTransactionInput input)
        {
            using (_cardRepository.DisableTracking())
            {
                var card = await _cardManager.GetCard(input.CardId, CurrentUser.Id!.Value) 
                    ?? throw new UserFriendlyException(L["CardNotFound"]);

                using (_cardTransactionRepository.DisableTracking())
                {
                    var transaction = (await _cardTransactionRepository.GetQueryableAsync())
                        .WhereIf(!string.IsNullOrEmpty(input.Filter), x => EF.Functions.Like(x.Description, $"%{input.Filter}%"));
                    
                    var totalCount = await transaction.CountAsync();
                    var data = await transaction
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
            var card = await _cardManager.GetCard(input.Id, CurrentUser.Id!.Value) 
                ?? throw new UserFriendlyException(L["CardNotFound"]);

            await _cardManager.FundCard(card, input.Amount);
            //await _backgroundJobManager.EnqueueAsync(new FundCardJobArgs
            //{
            //    Supplier = card.Supplier,
            //    UserId = CurrentUser.Id!.Value,
            //    Amount = input.Amount
            //});
        }


        #region Admin Methods
        [Authorize(VCardOnAbpPermissions.AddCard)]
        public Task AddCard(AddCardInput input)
        {
            var card = ObjectMapper.Map<AddCardInput, Card>(input);
            return _cardRepository.InsertAsync(card);
        }
        #endregion
    }
}
