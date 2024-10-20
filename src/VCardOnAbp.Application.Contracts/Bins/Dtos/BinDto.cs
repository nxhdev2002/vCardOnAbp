using System;

namespace VCardOnAbp.Bins.Dtos;

public class BinDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Currency { get; set; }
    public char? Symbol { get; set; }
    public decimal CreationFixedFee { get; set; }
    public decimal CreationPercentFee { get; set; }
    public decimal FundingFixedFee { get; set; }
    public decimal FundingPercentFee { get; set; }
}
