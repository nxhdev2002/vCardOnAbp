using System;

namespace VCardOnAbp.Cards.Dto;

public class CardTransactionDto
{
    public Guid CardId { get; private set; }
    public decimal AuthAmount { get; private set; }
    public string? Currency { get; private set; }
    public string? Description { get; private set; }
    public string? MerchantName { get; private set; }
    public decimal? SettleAmount { get; private set; }
    public string? Status { get; private set; }
    public string? Type { get; private set; }
    public DateTime CreationTime { get; private set; }

}
