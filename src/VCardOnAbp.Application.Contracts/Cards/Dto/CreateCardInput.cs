using System;

namespace VCardOnAbp.Cards.Dto;

public class CreateCardInput
{
    public decimal Amount { get; set; }
    public string CardName { get; set; }
    public Guid BinId { get; set; }
    public string? Note { get; set; }
}
