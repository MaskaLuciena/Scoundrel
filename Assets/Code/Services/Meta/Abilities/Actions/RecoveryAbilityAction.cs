using UnityEngine;

// Recovery — сброс порога оружия
public class RecoveryAbilityAction : IAbilityAction
{
    private readonly PlayerStatsModel _player;

    public RecoveryAbilityAction(PlayerStatsModel player)
    {
        _player = player;
    }

    public void Apply()
    {
        _player.ResetWeaponThreshold();
        Debug.Log("[Ability] Активирован Recovery — порог оружия восстановлен до 14");
    }
}
