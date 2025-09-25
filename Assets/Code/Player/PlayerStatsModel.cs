using System;
using UnityEngine;
using Zenject;

public class PlayerStatsModel
{
    public int MaxHP { get; private set; }
    public int CurrentHP { get; private set; }
    public CardData CurrentWeapon { get; private set; }
    public int WeaponPower { get; private set; }
    public int WeaponThreshold { get; private set; } = 14;

    private bool _healBoostActive = false;

    public event Action<int> OnHPChanged;
    public event Action<CardData> OnWeaponChanged;
    public event Action<int> OnWeaponThresholdChanged;
    public event Action OnPlayerDied;

    [Inject]
    public PlayerStatsModel(int maxHP)
    {
        MaxHP = maxHP;
        CurrentHP = MaxHP;
    }

    //бой
    public void FightEnemyWithWeapon(CardData enemy)
    {
        if (CurrentWeapon == null)
        {
            TakeDamage(enemy.value);
            return;
        }

        int damage = Mathf.Max(0, enemy.value - WeaponPower);

        // обновляем порог = сила врага - 1
        WeaponThreshold = Mathf.Max(1, enemy.value - 1);
        OnWeaponThresholdChanged?.Invoke(WeaponThreshold);

        TakeDamage(damage);
    }

    public void FightEnemyWithoutWeapon(CardData enemy)
    {
        TakeDamage(enemy.value);
    }

    private void TakeDamage(int damage)
    {
        if (damage <= 0) return;
        CurrentHP = Mathf.Max(0, CurrentHP - damage);
        OnHPChanged?.Invoke(CurrentHP);

        if (CurrentHP <= 0)
            OnPlayerDied?.Invoke();
    }

    //хил
    public void Heal(int health)
    {
        if (health <= 0) return;

        int finalHeal = _healBoostActive ? health * 2 : health;
        _healBoostActive = false;

        CurrentHP = Mathf.Min(MaxHP, CurrentHP + finalHeal);
        OnHPChanged?.Invoke(CurrentHP);
    }

    public void ActivateHealBoost()
    {
        _healBoostActive = true;
    }

    //оружие
    public void SetWeapon(CardData weapon)
    {
        CurrentWeapon = weapon;
        WeaponPower = weapon != null ? weapon.value : 0;

        WeaponThreshold = 14;
        OnWeaponChanged?.Invoke(CurrentWeapon);
        OnWeaponThresholdChanged?.Invoke(WeaponThreshold);
    }

    public void ResetWeaponThreshold()
    {
        WeaponThreshold = 14;
        OnWeaponThresholdChanged?.Invoke(WeaponThreshold);
    }

    //сейв
    public void SetState(int currentHP, int weaponThreshold, CardData weapon)
    {
        CurrentHP = currentHP;
        CurrentWeapon = weapon;
        WeaponPower = weapon != null ? weapon.value : 0;
        WeaponThreshold = weaponThreshold;

        OnHPChanged?.Invoke(CurrentHP);
        OnWeaponChanged?.Invoke(CurrentWeapon);
        OnWeaponThresholdChanged?.Invoke(WeaponThreshold);

        if (CurrentHP <= 0)
            OnPlayerDied?.Invoke();
    }
}
