using System;
using VCardOnAbp.Cards;

namespace VCardOnAbp.Bins.Dtos;

public class CreateBinDtoInput
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Supplier Supplier { get; set; }
    public Guid CurrencyId { get; set; }
}
