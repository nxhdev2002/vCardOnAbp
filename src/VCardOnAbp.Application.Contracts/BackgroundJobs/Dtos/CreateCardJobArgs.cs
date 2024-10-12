using System;
using VCardOnAbp.Cards;

namespace VCardOnAbp.BackgroundJobs.Dtos;

public class CreateCardJobArgs
{
    public string? CardName { get; set; }
    public Supplier Supplier { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
}
