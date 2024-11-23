namespace VCardOnAbp.ApiServices.Vcc51.Dtos;
public class Vcc51CardTransactionDto
{
    public virtual string? SerialNumber { get; set; }
    public virtual string? Merchant { get; set; }
    public virtual string? Type { get; set; }
    public virtual string? Amount { get; set; }
    public virtual string? AuthorizationAmt { get; set; }
    public virtual string? RefundAmt { get; set; }
    public virtual string? TransactionTime { get; set; }
    public virtual string? Description { get; set; }
}
