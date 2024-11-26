using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using VCardOnAbp.Currencies;
using VCardOnAbp.Masters;
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
    IRepository<UserTransaction, Guid> userTransRepository
) : DomainService
{
    private readonly ICardRepository _cardsRepository = cardsRepository;
    private readonly IRepository<Currency, Guid> _currencyRepo = currencyRepo;
    private readonly IRepository<Bin, Guid> _binRepo = binRepo;
    private readonly UserManager<Volo.Abp.Identity.IdentityUser> _userManager = userManager;
    private readonly IRepository<UserTransaction, Guid> _userTransRepository = userTransRepository;

    /// <summary>
    /// Create a new card
    /// </summary>
    /// <param name="CardNo"></param>
    /// <param name="BinId"></param>
    /// <param name="SupplierId"></param>
    /// <param name="SupplierIdentity"></param>
    /// <param name="CardName"></param>
    /// <param name="cardStatus"></param>
    /// <param name="Amount"></param>
    /// <param name="OwnerId"></param>
    /// <param name="Remark"></param>
    /// <returns></returns>
    /// <exception cref="BusinessException"></exception>
    public async Task<Card?> CreateCard(string CardNo, Guid BinId, string SupplierIdentity, string CardName, CardStatus cardStatus, decimal Amount, Guid OwnerId, string? Remark)
    {
        var cardId = GuidGenerator.Create();
        Logger.LogInformation($"{nameof(CreateCard)}: User {OwnerId} create card with Id: {cardId}, Amount: {Amount}");

        if (Amount <= 0) throw new BusinessException(VCardOnAbpDomainErrorCodes.AmountMustBePositive);
        var user = await _userManager.FindByIdAsync(OwnerId.ToString());
        if (user == null || !user.IsActive) throw new BusinessException(VCardOnAbpDomainErrorCodes.UserNotFound);
        var bin = await (await _binRepo.GetQueryableAsync()).FirstOrDefaultAsync(x => x.Id == BinId) ?? throw new BusinessException(VCardOnAbpDomainErrorCodes.BinNotFound);

        Currency currency = await (await _currencyRepo.GetQueryableAsync()).AsNoTracking().FirstOrDefaultAsync(x => x.Id == bin.CurrencyId) ?? throw new BusinessException(VCardOnAbpDomainErrorCodes.CurrencyNotFound);
        decimal usdRate = Amount * currency.UsdRate;

        var userBalance = user.GetProperty<decimal>(UserConsts.Balance);
        var requireBalance = bin.CreationFixedFee + (bin.CreationPercentFee * usdRate / 100);

        if (userBalance <= requireBalance + VCardOnAbpConsts.FronzeBalance) throw new BusinessException(VCardOnAbpDomainErrorCodes.InsufficientBalance);
        Logger.LogInformation($"{nameof(CreateCard)}: User {OwnerId} validate successfully with Id: {cardId}, Amount: {Amount}");
        user.SetProperty(UserConsts.Balance, userBalance - requireBalance);
        await _userManager.UpdateAsync(user);

        await _userTransRepository.InsertAsync(new UserTransaction(
            GuidGenerator.Create(), OwnerId, cardId, "Create Card", UserTransactionType.CreateCard, requireBalance
        ));

        return new Card(cardId, CardNo, BinId, bin.Supplier, SupplierIdentity, cardStatus, Amount, CardName, OwnerId, Remark);
    }

    public async Task Delete(Card card)
    {
        await _cardsRepository.DeleteAsync(card);
    }

    public void Lock(Card card)
    {
        card.ChangeStatus(CardStatus.Lock);
    }

    public async Task<Card> GetCard(Guid cardId, Guid userId, bool isNoTracking = false)
    {
        Card? card = await _cardsRepository.GetCard(cardId, userId, isNoTracking) ?? throw new BusinessException(VCardOnAbpDomainErrorCodes.CardNotFound);
        if (!isNoTracking) card.SetLastView(DateTime.UtcNow);
        return card;
    }

    public async Task FundCard(Card card, decimal amount)
    {
        if (card == null) throw new BusinessException(VCardOnAbpDomainErrorCodes.CardNotFound);
        if (amount < 0) throw new BusinessException(VCardOnAbpDomainErrorCodes.AmountMustBePositive)
                .WithData(nameof(amount), amount);

        Bin bin = await (await _binRepo.GetQueryableAsync()).AsNoTracking().FirstOrDefaultAsync(x => x.Id == card.BinId) ?? throw new BusinessException(VCardOnAbpDomainErrorCodes.BinNotFound);
        if (!bin.IsActive) throw new BusinessException(VCardOnAbpDomainErrorCodes.BinNotActive);

        IdentityUser? user = await _userManager.FindByIdAsync(card.CreatorId.ToString()!);
        if (user == null || !user.IsActive) throw new BusinessException(VCardOnAbpDomainErrorCodes.UserNotFound);

        Currency currency = await (await _currencyRepo.GetQueryableAsync()).AsNoTracking().FirstOrDefaultAsync(x => x.Id == bin.CurrencyId) ?? throw new BusinessException(VCardOnAbpDomainErrorCodes.CurrencyNotFound);
        decimal usdRate = amount * currency.UsdRate;

        var userBalance = user.GetProperty<decimal>(UserConsts.Balance);
        var requireBalance = bin.FundingFixedFee + (bin.FundingPercentFee * usdRate / 100);

        if (userBalance <= requireBalance + VCardOnAbpConsts.FronzeBalance) throw new BusinessException(VCardOnAbpDomainErrorCodes.InsufficientBalance);
        user.SetProperty(UserConsts.Balance, userBalance - requireBalance);
        await _userManager.UpdateAsync(user);

        user!.SetProperty(nameof(UserConsts.Balance), user!.GetProperty<decimal>(nameof(UserConsts.Balance)) - usdRate);
        card.SetLastView(DateTime.UtcNow);
        card.SetBalance(amount);
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
