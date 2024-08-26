using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCardOnAbp.Cards.Dto;
using VCardOnAbp.Security;
using Volo.Abp.Domain.Repositories;

namespace VCardOnAbp.Cards
{
    public class CardAppService(
        CardManager cardManager,
        IDataValidatorAppService dataValidatorAppService,
        IRepository<Card, Guid> cardRepository
    ) : VCardOnAbpAppService, ICardAppService
    {
        private readonly CardManager _cardManager = cardManager;
        private readonly IDataValidatorAppService _dataValidatorAppService = dataValidatorAppService;
        private readonly IRepository<Card, Guid> _cardRepository = cardRepository;
        public virtual async Task<CardDto> Create(CreateCardInput input)
        {
            var card = _cardManager.CreateCard("123", GuidGenerator.Create(), "1");
            await _cardRepository.InsertAsync(card);

            return ObjectMapper.Map<Card, CardDto>(card);
        }

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
