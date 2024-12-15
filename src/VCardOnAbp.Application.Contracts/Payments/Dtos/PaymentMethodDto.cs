using System.Collections.Generic;

namespace VCardOnAbp.Payments.Dtos;
public class PaymentMethodDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsEnabled { get; set; }
    public decimal FixedFee { get; set; }
    public decimal PercentageFee { get; set; }
    public GatewayType GatewayType { get; set; }
    public string? GuideContent { get; set; }
    public decimal MinAmount { get; set; }
    public List<GatewayRowActions> RowActions { get; set; } = new();
}
