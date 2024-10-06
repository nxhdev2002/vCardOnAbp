using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace VCardOnAbp.Cards
{
    public class CardManager(
        ICardRepository cardsRepository,
        UserManager<IdentityUser> userManager
    ) : DomainService
    {
        private readonly ICardRepository _cardsRepository = cardsRepository;
        private readonly UserManager<IdentityUser> _userManager = userManager;
        public Card CreateCard(string CardNo, Supplier SupplierId, string SupplierIdentity, CardStatus cardStatus = CardStatus.Active, decimal Balance = 0)
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

        public async Task<Card?> GetCard(Guid cardId, Guid userId)
        {
            var card = await _cardsRepository.GetCard(cardId, userId);
            if (card == null) return null;
            return card;
        }
    }
}
