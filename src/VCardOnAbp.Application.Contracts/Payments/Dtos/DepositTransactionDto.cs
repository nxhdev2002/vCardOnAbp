using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace VCardOnAbp.Payments.Dtos;
public class DepositTransactionDto
{
    public int PaymentMethodId { get; private set; }
    public decimal Amount { get; private set; }
    public DepositTransactionStatus TransactionStatus { get; private set; }
    public DateTime? ApprovedAt { get; private set; }
    [MaxLength(500)]
    public string? Comment { get; private set; }
    public DateTime? CreationTime { get; private set; }
}

public class GetDepositTransactionInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}