public interface ICardHandler
{
    CardType Type { get; }
    void Handle(CardView view, CardData card);
}