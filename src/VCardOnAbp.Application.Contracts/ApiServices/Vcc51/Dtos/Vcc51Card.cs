namespace VCardOnAbp.ApiServices.Vcc51.Dtos;
public class Vcc51Card
{
    public string? CardNo { get; set; }
    public string? Cvv { get; set; }
    public string? Amount { get; set; }
    public string? Status { get; set; }
    public string? Exp { get; set; }
    public string? Remark { get; set; }
    public string? CreationTime { get; set; }
    public string? Currency { get; set; }
    public string? TotalAmount { get; set; }
    public string? RemainAmount { get; set; }
    public string? CardName { get; set; }
}
