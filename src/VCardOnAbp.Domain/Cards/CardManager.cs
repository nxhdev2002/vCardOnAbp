using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using VCardOnAbp.Currencies;
using VCardOnAbp.Masters;
using VCardOnAbp.Security;
using VCardOnAbp.Transactions;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;

namespace VCardOnAbp.Cards;

public class CardManager(
    ICardRepository cardsRepository,
    IRepository<Currency, Guid> currencyRepo,
    IRepository<Bin, Guid> binRepo,
    UserManager<Volo.Abp.Identity.IdentityUser> userManager,
    IRepository<UserTransaction, Guid> userTransRepository,
    SecurityManager securityManager
) : DomainService
{
    private readonly ICardRepository _cardsRepository = cardsRepository;
    private readonly IRepository<Currency, Guid> _currencyRepo = currencyRepo;
    private readonly IRepository<Bin, Guid> _binRepo = binRepo;
    private readonly UserManager<Volo.Abp.Identity.IdentityUser> _userManager = userManager;
    private readonly IRepository<UserTransaction, Guid> _userTransRepository = userTransRepository;
    private readonly SecurityManager _securityManager = securityManager;

    public Card CreateCard(string CardNo, Guid BinId, Supplier SupplierId, string SupplierIdentity, string CardName, CardStatus cardStatus = CardStatus.Active, decimal Balance = 0)
    {
        // TODO: Return business exception
        if (Balance < 0) return null;

        return new Card(GuidGenerator.Create(), CardNo, BinId, SupplierId, SupplierIdentity, cardStatus, Balance, CardName);
    }

    public async Task Delete(Card card)
    {
        await _cardsRepository.DeleteAsync(card);
    }

    public void Lock(Card card)
    {
        card.ChangeStatus(CardStatus.Lock);
    }

    public async Task<Card> GetCard(Guid cardId, Guid userId, bool isNoTracking = true)
    {
        Card? card = await _cardsRepository.GetCard(cardId, userId, isNoTracking) ?? throw new BusinessException(VCardOnAbpDomainErrorCodes.CardNotFound);
        card.SetLastView(DateTime.UtcNow);
        return card;
    }

    public async Task FundCard(Card card, decimal amount)
    {
        if (card == null) throw new BusinessException(VCardOnAbpDomainErrorCodes.CardNotFound);
        if (amount < 0) throw new BusinessException(VCardOnAbpDomainErrorCodes.AmountMustBePositive)
                .WithData(nameof(amount), amount);

        Bin bin = await (await _binRepo.GetQueryableAsync()).AsNoTracking().FirstOrDefaultAsync(x => x.Id == card.BinId) ?? throw new BusinessException(VCardOnAbpDomainErrorCodes.BinNotFound);
        if (!bin.IsActive) throw new BusinessException(VCardOnAbpDomainErrorCodes.BinNotActive);

        Currency currency = await (await _currencyRepo.GetQueryableAsync()).AsNoTracking().FirstOrDefaultAsync(x => x.Id == bin.CurrencyId) ?? throw new BusinessException(VCardOnAbpDomainErrorCodes.CurrencyNotFound);
        decimal usdRate = amount * currency.UsdRate;

        IdentityUser? user = await _userManager.FindByIdAsync(card.CreatorId.ToString()!);
        user!.SetProperty(nameof(UserConsts.Balance), user!.GetProperty<decimal>(nameof(UserConsts.Balance)) - usdRate);
        card.SetLastView(DateTime.UtcNow);
        card.SetBalance(amount);

        await _userTransRepository.InsertAsync(
            new UserTransaction(GuidGenerator.Create(), card.CreatorId!.Value, card.Id, null, UserTransactionType.FundCard, amount)
        );
    }

    public async Task DeleteAsync(Card card)
    {
        await _cardsRepository.DeleteAsync(card);
    }

    public async Task<(string, string)> ViewCardSecret(Guid cardId, Guid userId)
    {
        Card? card = await GetCard(cardId, userId);
        if (card == null) throw new BusinessException(VCardOnAbpDomainErrorCodes.CardNotFound);

        return (card.Cvv, card.ExpirationTime);
    }
}
