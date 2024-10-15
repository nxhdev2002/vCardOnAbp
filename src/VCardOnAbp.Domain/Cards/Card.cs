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
    [MaxLength(50)]
    public string SupplierIdentity { get; private set; }
    public CardStatus CardStatus { get; private set; }
    public DateTime? LastView { get; private set; }

    private Card() { }
    public Card(Guid id, string cardNo, Guid binId, Supplier supplierId, string supplierIdentity, CardStatus cardStatus, decimal balance) : base(id)
    {
        CardNo = cardNo;
        BinId = binId;
        Supplier = supplierId;
        SupplierIdentity = supplierIdentity;
        CardStatus = cardStatus;
        Balance = balance;
    }

    public Card ChangeStatus(CardStatus cardStatus)
    {
        var previous = CardStatus;
        CardStatus = cardStatus;
        AddLocalEvent(new CardStatusChangedEvent(this, previous, cardStatus));
        return this;
    }

    public Card SetBalance(decimal balance)
    {
        if (Balance + balance < 0)
        {
            throw new BusinessException();
        }
        Balance = balance;
        return this;
    }

    public Card SetLastView(DateTime lastView)
    {
        LastView = lastView;
        return this;
    }
}
