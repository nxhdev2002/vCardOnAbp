namespace VCardOnAbp.Cards;

public class CardFundedEvent
{
    public Card Card { get; }

    public decimal Amount { get; }

    public CardFundedEvent(Card card, decimal amount)
    {
        Card = card;
        Amount = amount;
    }
}
