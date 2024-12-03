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

    public decimal MinAmount { get; private set; }
    public GatewayType GatewayType { get; private set; }
    public string GuideContent { get; private set; }
    private PaymentMethod()
    {
    }

    public PaymentMethod(
        string name,
        string description,
        decimal fixedFee,
        decimal percentageFee,
        string guideContent,
        bool isEnabled = true,
        GatewayType gatewayType = GatewayType.MANUAL,
        decimal minAmount = 0
    )
    {
        if (fixedFee < 0) throw new BusinessException(VCardOnAbpDomainErrorCodes.InvalidAmount).WithData(nameof(fixedFee), fixedFee);
        if (percentageFee < 0) throw new BusinessException(VCardOnAbpDomainErrorCodes.InvalidAmount).WithData(nameof(percentageFee), percentageFee);
        if (minAmount < 0) throw new BusinessException(VCardOnAbpDomainErrorCodes.InvalidAmount).WithData(nameof(minAmount), minAmount);
        if (Enum.IsDefined(typeof(GatewayType), gatewayType) == false) throw new BusinessException(VCardOnAbpDomainErrorCodes.InvalidGatewayType).WithData(nameof(gatewayType), gatewayType);


        Name = name;
        Description = description;
        IsEnabled = isEnabled;
        FixedFee = fixedFee;
        PercentageFee = percentageFee;
        GuideContent = guideContent;
        GatewayType = gatewayType;
        MinAmount = minAmount;
    }
}
