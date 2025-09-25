using UnityEngine;

public class HealCardHandler : ICardHandler
{
    private readonly PlayerStatsModel _stats;
    private readonly RoomModel _room;

    public CardType Type => CardType.Heal;

    public HealCardHandler(PlayerStatsModel stats, RoomModel room)
    {
        _stats = stats;
        _room = room;
    }

    public void Handle(CardView view, CardData card)
    {
        if (!_room.HealUsedThisRoom)
        {
            _stats.Heal(card.value);
            _room.HealUsedThisRoom = true;
        }
    }
}