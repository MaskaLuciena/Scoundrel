using UnityEngine;

public class EnemyCardHandler : ICardHandler
{
    private readonly PlayerStatsModel _stats;
    private readonly RoomModel _room;
    private readonly RoomView _view;
    private readonly RoomFlowHandler _roomFlow;

    public CardType Type => CardType.Enemy;

    public EnemyCardHandler(PlayerStatsModel stats, RoomModel room, RoomView view, RoomFlowHandler roomFlow)
    {
        _stats = stats;
        _room = room;
        _view = view;
        _roomFlow = roomFlow;
    }

    public void Handle(CardView view, CardData card)
    {
        if (_stats.CurrentWeapon != null && card.value <= _stats.WeaponThreshold)
        {
            _view.ShowEnemyChoice(
                card, //сама карта врага
                () =>
                {
                    _stats.FightEnemyWithWeapon(card);
                    _room.RemoveCard(card);
                    _roomFlow.AfterCardResolved(view, card);
                },
                () =>
                {
                    _stats.FightEnemyWithoutWeapon(card);
                    _room.RemoveCard(card);
                    _roomFlow.AfterCardResolved(view, card);
                }
            );
        }
        else
        {
            _stats.FightEnemyWithoutWeapon(card);
            _room.RemoveCard(card);
            _roomFlow.AfterCardResolved(view, card);
        }
    }
}