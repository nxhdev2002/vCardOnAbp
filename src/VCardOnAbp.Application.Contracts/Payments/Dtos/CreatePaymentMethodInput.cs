namespace VCardOnAbp.Payments.Dtos;
public record CreatePaymentMethodInput
(
    string Name,
    string Description,
    decimal FixedFee,
    decimal PercentageFee,
    GatewayType GatewayType,
    string GuideContent,
    decimal MinAmount
);