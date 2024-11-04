using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace VCardOnAbp.Payments;
public class PaymentMethod : Entity<int>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsEnabled { get; private set; }
    public decimal FixedFee { get; private set; }
    public decimal PercentageFee { get; private set; }

    public GatewayType GatewayType { get; private set; }
    private PaymentMethod()
    {
    }

    public PaymentMethod(
        string name,
        string description,
        decimal fixedFee,
        decimal percentageFee,
        bool isEnabled = true,
        GatewayType gatewayType = GatewayType.MANUAL
    )
    {
        if (fixedFee < 0) throw new BusinessException(VCardOnAbpDomainErrorCodes.AmountMustBePositive).WithData(nameof(fixedFee), fixedFee);
        if (percentageFee < 0) throw new BusinessException(VCardOnAbpDomainErrorCodes.AmountMustBePositive).WithData(nameof(percentageFee), percentageFee);
        if (Enum.IsDefined(typeof(GatewayType), gatewayType) == false) throw new BusinessException(VCardOnAbpDomainErrorCodes.InvalidGatewayType).WithData(nameof(gatewayType), gatewayType);

        Name = name;
        Description = description;
        IsEnabled = isEnabled;
        FixedFee = fixedFee;
        PercentageFee = percentageFee;
        GatewayType = gatewayType;
    }
}
