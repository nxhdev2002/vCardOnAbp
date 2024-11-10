using System;
using VCardOnAbp.Transactions;

namespace VCardOnAbp.Accounts.Dtos;
public class UserTransactionDto
{
    public DateTime CreationTime { get; private set; }
    public Guid UserId { get; private set; }
    public Guid RelatedEntity { get; private set; }
    public string? Description { get; private set; }
    public decimal Amount { get; private set; }
    public UserTransactionType Type { get; private set; }
}
