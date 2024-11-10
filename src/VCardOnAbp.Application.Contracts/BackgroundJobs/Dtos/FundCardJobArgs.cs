using System;
using VCardOnAbp.Cards;

namespace VCardOnAbp.BackgroundJobs.Dtos;

public class FundCardJobArgs
{
    public Supplier Supplier { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public Guid CardId { get; set; }
}
