using UnityEngine;

// Heal — удвоение лечения
public class HealAbilityAction : IAbilityAction
{
    private readonly PlayerStatsModel _player;

    public HealAbilityAction(PlayerStatsModel player)
    {
        _player = player;
    }

    public void Apply()
    {
        _player.ActivateHealBoost();
        Debug.Log("[Ability] Активирован Heal — удвоение лечения");
    }
}

