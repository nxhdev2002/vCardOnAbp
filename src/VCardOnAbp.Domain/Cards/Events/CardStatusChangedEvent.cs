namespace VCardOnAbp.Cards.Events;
public class CardStatusChangedEvent(Card card, CardStatus previousStatus, CardStatus currentStatus)
{
    public Card Card { get; set; } = card;
    public CardStatus PreviousStatus { get; set; } = previousStatus;
    public CardStatus CurrentStatus { get; set; } = currentStatus;
}
