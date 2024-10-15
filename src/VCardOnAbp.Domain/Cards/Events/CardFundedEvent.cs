namespace VCardOnAbp.Cards.Events;

public class CardFundedEvent(Card card, decimal amount)
{
    public Card Card { get; } = card;

    public decimal Amount { get; } = amount;
}
