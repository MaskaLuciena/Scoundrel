public class WeaponCardHandler : ICardHandler
{
    private readonly PlayerStatsModel _stats;

    public CardType Type => CardType.Weapon;

    public WeaponCardHandler(PlayerStatsModel stats)
    {
        _stats = stats;
    }

    public void Handle(CardView view, CardData card)
    {
        _stats.SetWeapon(card);
    }
}