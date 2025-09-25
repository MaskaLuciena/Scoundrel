using Zenject;

public class AbilityService
{
    private readonly RunPointsService _runPoints;
    private readonly PlayerStatsModel _player;
    private readonly PlayerController _controller;

    [Inject]
    public AbilityService(RunPointsService runPoints, PlayerStatsModel player, PlayerController controller)
    {
        _runPoints = runPoints;
        _player = player;
        _controller = controller;
    }

    public bool TryUseAbility(MetaActiveAbility ability)
    {
        if (!_runPoints.TrySpendPoints(ability.Cost))
            return false;

        IAbilityAction action = null;

        switch (ability.Id)
        {
            case "heal":
                action = new HealAbilityAction(_player);
                break;

            case "escape":
                action = new EscapeAbilityAction(_controller);
                break;

            case "recovery":
                action = new RecoveryAbilityAction(_player);
                break;
        }

        action?.Apply();
        return true;
    }
}