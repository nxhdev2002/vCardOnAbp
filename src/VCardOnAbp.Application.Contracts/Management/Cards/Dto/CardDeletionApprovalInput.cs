namespace VCardOnAbp.Management.Cards.Dto;
public class CardDeletionApprovalInput 
{
    public bool IsApproved { get; set; }
    public decimal? RefundAmount { get; set; }
}
