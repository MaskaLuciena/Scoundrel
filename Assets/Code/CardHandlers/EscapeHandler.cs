using UnityEngine;

public class EscapeHandler
{
    private readonly DeckModel _deck;
    private readonly RoomModel _room;
    private readonly RunPointsService _points;
    private readonly SaveLoadManager _saves;
    private readonly RoomView _view;

    public EscapeHandler(DeckModel deck, RoomModel room, RunPointsService points, SaveLoadManager saves, RoomView view)
    {
        _deck = deck;
        _room = room;
        _points = points;
        _saves = saves;
        _view = view;
    }

    public void TryEscape()
    {
        if (_room.LastActionWasEscape)
        {
            Debug.Log("Нельзя сбегать два раза подряд!");
            return;
        }

        if (_view.ActiveCardCount() < 4)
        {
            Debug.Log("Побег невозможен — карты уже разыграны!");
            return;
        }

        DoEscape(nextRoomEscape: true);
    }

    public void ForceEscape()
    {
        DoEscape(nextRoomEscape: false);
    }

    private void DoEscape(bool nextRoomEscape)
    {
        _deck.PutBackBottom(_room.GetCards());
        var newCards = _deck.DrawCards(4);
        _room.SetCards(newCards);

        _room.LastActionWasEscape = !nextRoomEscape;
        _room.HealUsedThisRoom = false;

        if (nextRoomEscape)
            _points.AddPointForRoom(true);

        _saves.SaveGame();
    }
}