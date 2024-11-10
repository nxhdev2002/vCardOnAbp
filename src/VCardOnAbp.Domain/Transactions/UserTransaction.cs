using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Entities;

namespace VCardOnAbp.Transactions;

[Index(nameof(Description), Name = "IX_UserTransaction_Description")]
public class UserTransaction : Entity<Guid>
{
    public DateTime CreationTime { get; private set; }
    public Guid UserId { get; private set; }
    public Guid RelatedEntity { get; private set; }
    public string? Description { get; private set; }
    public decimal Amount { get; private set; }
    public UserTransactionType Type { get; private set; }
    private UserTransaction() { }

    public UserTransaction(Guid id, Guid userId, Guid relatedEntity, string? description, UserTransactionType type, decimal amount = 0) : base(id)
    {
        UserId = userId;
        Description = description;
        RelatedEntity = relatedEntity;
        Amount = amount;
        Type = type;
        CreationTime = DateTime.Now;
    }
}
