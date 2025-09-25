using System.Collections.Generic;
using UnityEngine;

public class RoomFlowHandler
{
    private readonly DeckModel _deck;
    private readonly RoomModel _room;
    private readonly SaveLoadManager _saves;
    private readonly RunPointsService _points;
    private readonly RoomView _view;

    public RoomFlowHandler(DeckModel deck, RoomModel room, SaveLoadManager saves, RunPointsService points, RoomView view)
    {
        _deck = deck;
        _room = room;
        _saves = saves;
        _points = points;
        _view = view;
    }

    public void ShowNewRoom()
    {
        var cards = _deck.DrawCards(4);
        _room.SetCards(cards);
        _room.HealUsedThisRoom = false;
        _room.LastActionWasEscape = false;
        _saves.SaveGame();
    }

    public void AfterCardResolved(CardView view, CardData card)
    {
        // обновляем модель
        var updated = new List<CardData>(_room.CurrentCards);
        updated.Remove(card);
        _room.SetCards(updated);

        if (_view.ActiveCardCount() == 1)
        {
            var newCards = _deck.DrawCards(3);
            updated.AddRange(newCards);
            _room.SetCards(updated);

            _room.HealUsedThisRoom = false;
            _points.AddPointForRoom(false);
            _saves.SaveGame();
        }

        _room.LastActionWasEscape = false;
    }
}