using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace VCardOnAbp.Currencies;

public class Currency : FullAuditedEntity<Guid>
{
    public string CurrencyName { get; private set; }
    public string CurrencyCode { get; private set; }
    public char CurrencySymbol { get; private set; }
    public decimal UsdRate { get; private set; }

    private Currency()
    {
    }

    public Currency(Guid id, string currencyName, string currencyCode, char currencySymbol, decimal usdRate) : base(id)
    {
        CurrencyName = currencyName;
        CurrencyCode = currencyCode;
        CurrencySymbol = currencySymbol;
        UsdRate = usdRate;
    }
}
