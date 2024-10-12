namespace VCardOnAbp.Cards.Dto;

public class CreateCardInput
{
    public decimal Amount { get; set; }
    public Supplier Supplier { get; set; }
    public string CardName { get; set; }
}
