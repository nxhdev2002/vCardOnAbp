using System;
using System.ComponentModel.DataAnnotations;
using VCardOnAbp.Cards;
using Volo.Abp.Domain.Entities;

namespace VCardOnAbp.Masters;

public class Bin : Entity<Guid>
{
    [MaxLength(100)]
    public string Name { get; private set; }
    [MaxLength(500)]
    public string? Description { get; private set; }
    [MaxLength(30)]
    public string? BinMapping { get; private set; }
    public Supplier Supplier { get; private set; }
    public Guid CurrencyId { get; private set; }
    public decimal CreationFixedFee { get; private set; }
    public decimal CreationPercentFee { get; private set; }
    public decimal FundingFixedFee { get; private set; }
    public decimal FundingPercentFee { get; private set; }

    public bool IsActive { get; private set; }

    private Bin() { }
    public Bin(
        Guid id,
        string name, string? desc, Supplier supplier, Guid currency,
        string? binMapping = null,
        decimal creationFixedFee = 0, decimal creationPercentFee = 0,
        decimal fundingFixedFee = 0, decimal fundingPercentFee = 0,
        bool isActive = true
    ) : base(id)
    {
        Name = name;
        Description = desc;
        Supplier = supplier;
        CurrencyId = currency;
        IsActive = isActive;
        BinMapping = binMapping;

        CreationFixedFee = creationFixedFee;
        CreationPercentFee = creationPercentFee;
        FundingFixedFee = fundingFixedFee;
        FundingPercentFee = fundingPercentFee;

    }

    public Bin UpdateFee(decimal? creationFixedFee = null, decimal? creationPercentFee = null,
        decimal? fundingFixedFee = null, decimal? fundingPercentFee = null)
    {
        CreationFixedFee = creationFixedFee ?? CreationFixedFee;
        CreationPercentFee = creationPercentFee ?? CreationPercentFee;
        FundingFixedFee = fundingFixedFee ?? FundingFixedFee;
        FundingPercentFee = fundingPercentFee ?? FundingPercentFee;
        return this;
    }

    public Bin SetCurrency(Guid currency)
    {
        CurrencyId = currency;
        return this;
    }

    public Bin SetBinMapping(string binMapping)
    {
        BinMapping = binMapping;
        return this;
    }
}
