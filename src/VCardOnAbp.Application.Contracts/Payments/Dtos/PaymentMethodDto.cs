namespace VCardOnAbp.Payments.Dtos;
public record PaymentMethodDto
(
    long Id,
    string Name,
    string Description,
    bool IsEnabled,
    decimal FixedFee,
    decimal PercentageFee,
    GatewayType GatewayType
);
