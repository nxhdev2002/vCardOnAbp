using System;
using System.Threading.Tasks;
using VCardOnAbp.BackgroundJobs.Dtos;
using VCardOnAbp.Cards.Dto;
using VCardOnAbp.Security;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain.Repositories;

namespace VCardOnAbp.Cards
{
    public class CardAppService(
        CardManager cardManager,
        IBackgroundJobManager backgroundJobManager,
        IRepository<Card, Guid> cardRepository
    ) : VCardOnAbpAppService, ICardAppService
    {
        private readonly CardManager _cardManager = cardManager;
        private readonly IRepository<Card, Guid> _cardRepository = cardRepository;
        private readonly IBackgroundJobManager _backgroundJobManager = backgroundJobManager;

        public virtual async Task Create(CreateCardInput input) => await _backgroundJobManager.EnqueueAsync(new CreateCardJobArgs
        {
            CardName = input.CardName,
            Supplier = input.Supplier,
            UserId = CurrentUser.Id.Value,
            Amount = input.Amount
        });

        public virtual async Task<object> Action(ActionInput input)
        {
            var card = await _cardManager.GetCard(input.Id);
            switch (input.Action)
            {
                case CardAction.LOCK:
                    _cardManager.Lock(card);
                    break;
                case CardAction.DELETE:
                    await _cardManager.Delete(card);
                    break;
                case CardAction.FUND:
                    FundCard(card, input.Value ?? 0);
                    break;
                case CardAction.VIEW:
                    return ObjectMapper.Map<Card, CardDto>(card);
                default:
                    return null;
            }
            return null;
        }
        #region Private Methods

        private void FundCard(Card card, decimal Balance)
        {
            card.SetBalance(Balance);
        }

        #endregion
    }
}
