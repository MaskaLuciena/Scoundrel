using UnityEngine;

// Escape — побег без ограничений
public class EscapeAbilityAction : IAbilityAction
{
    private readonly PlayerController _playerController;

    public EscapeAbilityAction(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public void Apply()
    {
        _playerController.ForceEscapeRoom();
        Debug.Log("[Ability] Активирован Escape — побег без ограничений");
    }
}
