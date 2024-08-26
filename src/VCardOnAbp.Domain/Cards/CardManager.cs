using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace VCardOnAbp.Cards
{
    public class CardManager(IRepository<Card, Guid> cardsRepository) : DomainService
    {
        private readonly IRepository<Card, Guid> _cardsRepository = cardsRepository;
        public Card CreateCard(string CardNo, Guid SupplierId, string SupplierIdentity, CardStatus cardStatus = CardStatus.Active, decimal Balance = 0)
        {
            // TODO: Return business exception
            if (Balance < 0) return null;

            return new Card(CardNo, SupplierId, SupplierIdentity, cardStatus, Balance);
        }

        public async Task Delete(Card card)
        {
            await _cardsRepository.DeleteAsync(card);
        }

        public void Lock(Card card)
        {
            card.ChangeStatus(CardStatus.Lock);
        }

        public async Task<Card> GetCard(Guid cardId)
        {
            return await _cardsRepository.GetAsync(cardId);
        }
    }
}
