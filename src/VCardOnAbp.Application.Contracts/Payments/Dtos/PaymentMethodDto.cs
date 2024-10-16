namespace VCardOnAbp.Payments.Dtos;
public record PaymentMethodDto
(
    string Name,
    string Description,
    bool IsEnabled,
    decimal FixedFee,
    decimal PercentageFee
);
