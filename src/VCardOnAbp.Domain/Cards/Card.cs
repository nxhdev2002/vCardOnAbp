using System;
using System.ComponentModel.DataAnnotations;
using VCardOnAbp.Cards.Events;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace VCardOnAbp.Cards;

public class Card : FullAuditedAggregateRoot<Guid>
{
    [MaxLength(20)]
    public string CardNo { get; private set; }
    public decimal Balance { get; private set; }
    public Guid BinId { get; private set; }
    public Supplier Supplier { get; private set; }
    [MaxLength(500)]
    public string SupplierIdentity { get; private set; }
    public CardStatus CardStatus { get; private set; } = CardStatus.Active;
    public CardStatus? CardOldStatus { get; private set; }
    public DateTime? LastView { get; private set; }
    [MaxLength(50)]
    public string CardName { get; private set; }
    public Guid OwnerId { get; private set; }
    public DateTime? LastSync { get; set; }
    [MaxLength(500)]
    public string? Note { get; set; }
    #region SECRET
    public string? PublicKey { get; private set; }
    [MaxLength(500)]
    public string? Cvv { get; private set; }
    [MaxLength(500)]
    public string? ExpirationTime { get; private set; }
    #endregion

    private Card() { }
    public Card(Guid id, string cardNo, Guid binId, Supplier supplierId, string supplierIdentity, CardStatus cardStatus, decimal balance, string cardName, Guid ownerId, string? note = null) : base(id)
    {
        CardNo = cardNo;
        BinId = binId;
        Supplier = supplierId;
        SupplierIdentity = supplierIdentity;
        CardStatus = cardStatus;
        Balance = balance;
        CardName = cardName;
        OwnerId = ownerId;
        Note = note;
    }

    public Card ChangeStatus(CardStatus cardStatus)
    {
        CardOldStatus = CardStatus;
        CardStatus = cardStatus;
        AddLocalEvent(new CardStatusChangedEvent(this, CardOldStatus.Value, cardStatus));
        return this;
    }

    public Card SetBalance(decimal balance)
    {
        if (Balance + balance < 0)
        {
            throw new BusinessException();
        }
        Balance += balance;
        return this;
    }

    public Card SetLastView(DateTime lastView)
    {
        LastView = lastView;
        return this;
    }

    public Card SetSecret(string? publicKey, string? cvv, string? expirationTime)
    {
        PublicKey = publicKey;
        Cvv = cvv;
        ExpirationTime = expirationTime;
        return this;
    }

    public Card SetIdentifyKey(string identify)
    {
        SupplierIdentity = identify;
        return this;
    }

    public Card SetSyncInfo(string cardNo, string cvv, string exp)
    {
        CardNo = cardNo;
        Cvv = cvv;
        ExpirationTime = exp;
        LastSync = DateTime.UtcNow;
        return this;
    }

    public Card ChangeOwner(Guid ownerId)
    {
        OwnerId = ownerId;
        return this;
    }

    public Card SetNote(string note)
    {
        Note = note;
        return this;
    }
}
