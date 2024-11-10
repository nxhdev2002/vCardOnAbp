using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace VCardOnAbp.Payments;
public class DepositTransaction : FullAuditedAggregateRoot<Guid>
{
    public int PaymentMethodId { get; private set; }
    public decimal Amount { get; private set; }
    public DepositTransactionStatus TransactionStatus { get; private set; }
    public DateTime? ApprovedAt { get; private set; }
    [MaxLength(500)]
    public string? Comment { get; private set; }
    public Guid Requester { get; private set; }

    private DepositTransaction()
    {
    }

    public DepositTransaction(Guid id, int paymentMethodId, decimal amount, Guid requester)
    {
        Id = id;
        PaymentMethodId = paymentMethodId;
        Amount = amount;
        TransactionStatus = DepositTransactionStatus.Pending;
        Requester = requester;
    }

    public DepositTransaction FinishTransaction()
    {
        ApprovedAt = DateTime.UtcNow;
        TransactionStatus = DepositTransactionStatus.Completed;
        return this;
    }

    public DepositTransaction CancelTransaction()
    {
        TransactionStatus = DepositTransactionStatus.Failed;
        return this;
    }

    public DepositTransaction SetComment(string comment)
    {
        Comment = comment;
        return this;
    }
}
