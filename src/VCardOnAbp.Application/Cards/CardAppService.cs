using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCardOnAbp.Cards.Dto;
using Volo.Abp.Domain.Repositories;

namespace VCardOnAbp.Cards
{
    public class CardAppService(
        CardManager cardManager,
        IRepository<Card, Guid> cardRepository
    ) : VCardOnAbpAppService, ICardAppService
    {
        private readonly CardManager _cardManager = cardManager;
        private readonly IRepository<Card, Guid> _cardRepository = cardRepository;
        public async Task<CardDto> Create(CreateCardInput input)
        {
            var card = _cardManager.CreateCard("123", GuidGenerator.Create(), "1");
            await _cardRepository.InsertAsync(card);

            return ObjectMapper.Map<Card, CardDto>(card);
        }
    }
}
