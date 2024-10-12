using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace VCardOnAbp.Cards;

public class CardOwner : FullAuditedAggregateRoot<Guid>
{
    public Guid CardId { get; private set; }
    public Guid OwnerId { get; private set; }
    private CardOwner() { }
    public CardOwner(Guid cardId, Guid ownerId)
    {
        CardId = cardId;
        OwnerId = ownerId;
    }

    public CardOwner ChangeOwner(Guid ownerId)
    {
        OwnerId = ownerId;
        return this;
    }
}
