using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using VCardOnAbp.Currencies;
using VCardOnAbp.Masters;
using VCardOnAbp.Users;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;

namespace VCardOnAbp.Cards
{
    public class CardManager(
        ICardRepository cardsRepository,
        IRepository<UserCurrency, Guid> userCurrencyRepo,
        IRepository<Currency, Guid> currencyRepo,
        IRepository<Bin, Guid> binRepo,
        UserManager<Volo.Abp.Identity.IdentityUser> userManager
    ) : DomainService
    {
        private readonly ICardRepository _cardsRepository = cardsRepository;
        private readonly IRepository<UserCurrency, Guid> _userCurrencyRepo = userCurrencyRepo;
        private readonly IRepository<Currency, Guid> _currencyRepo = currencyRepo;
        private readonly IRepository<Bin, Guid> _binRepo = binRepo;
        private readonly UserManager<Volo.Abp.Identity.IdentityUser> _userManager = userManager;
        public Card CreateCard(string CardNo, Guid BinId, Supplier SupplierId, string SupplierIdentity, CardStatus cardStatus = CardStatus.Active, decimal Balance = 0)
        {
            // TODO: Return business exception
            if (Balance < 0) return null;

            return new Card(GuidGenerator.Create(), CardNo, BinId, SupplierId, SupplierIdentity, cardStatus, Balance);
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
            if (card == null) throw new BusinessException(VCardOnAbpDomainErrorCodes.CardNotFound);
            card.SetLastView(DateTime.UtcNow);
            return card;
        }

        public async Task FundCard(Card card, decimal amount)
        {
            if (amount < 0) throw new BusinessException(VCardOnAbpDomainErrorCodes.AmountMustBePositive)
                    .WithData(nameof(amount), amount);

            // adjust balance
            var bin = await (await _binRepo.GetQueryableAsync()).AsNoTracking().FirstOrDefaultAsync(x => x.Id == card.BinId);
            if (bin == null) throw new BusinessException(VCardOnAbpDomainErrorCodes.BinNotFound);

            var currency = await (await _currencyRepo.GetQueryableAsync()).AsNoTracking().FirstOrDefaultAsync(x => x.Id == bin.CurrencyId);
            if (currency == null) throw new BusinessException(VCardOnAbpDomainErrorCodes.CurrencyNotFound);

            var userCurrency = await (await _userCurrencyRepo.GetQueryableAsync()).AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == card.CreatorId && x.CurrencyId == currency.Id);

            if (userCurrency == null) throw new BusinessException();
            card.SetLastView(DateTime.UtcNow);
            card.SetBalance(-amount);

            userCurrency.Balance -= amount;
        }
    }
}
