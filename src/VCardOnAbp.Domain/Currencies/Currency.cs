using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace VCardOnAbp.Currencies;

public class Currency : FullAuditedEntity<Guid>
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public char Symbol { get; private set; }
    public decimal UsdRate { get; private set; }

    private Currency()
    {
    }

    public Currency(Guid id, string currencyName, string currencyCode, char currencySymbol, decimal usdRate) : base(id)
    {
        Name = currencyName;
        Code = currencyCode;
        Symbol = currencySymbol;
        UsdRate = usdRate;
    }
}
