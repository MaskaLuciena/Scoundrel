using System;
using System.Collections.Generic;
using Zenject;

public class RoomModel
{
    private readonly CardLoader _loader;
    private List<CardData> _currentCards = new();

    public event Action<List<CardData>> OnRoomCardsChanged;

    public IReadOnlyList<CardData> CurrentCards => _currentCards;

    //состояние комнаты
    public bool HealUsedThisRoom { get; set; }
    public bool LastActionWasEscape { get; set; }

    [Inject]
    public RoomModel(CardLoader loader)
    {
        _loader = loader;
    }

    public void SetCards(List<CardData> cards)
    {
        _currentCards = new List<CardData>(cards);
        OnRoomCardsChanged?.Invoke(new List<CardData>(_currentCards));
    }

    public void RemoveCard(CardData card)
    {
        _currentCards.Remove(card);
        OnRoomCardsChanged?.Invoke(new List<CardData>(_currentCards));
    }

    public void ResetRoomFlags()
    {
        HealUsedThisRoom = false;
        LastActionWasEscape = false;
    }

    public List<CardData> GetCards() => new List<CardData>(_currentCards);
    public void SetState(List<CardData> cards) => SetCards(cards);
}
