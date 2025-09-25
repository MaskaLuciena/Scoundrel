using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Хранит прогрессию игрока между забегами:
/// - опыт
/// - уровень
/// - открытые активки
/// </summary>
public class MetaProgressionModel
{
    public int CurrentXP { get; private set; }
    public int CurrentLevel { get; private set; }
    public int CurrentThreshold { get; private set; }

    // Список активок, которые игрок открыл
    public HashSet<MetaActiveAbility> UnlockedAbilities { get; private set; } = new();

    private readonly int[] _xpThresholds = { 50, 150, 300, 600 }; // XP на уровень

    public MetaProgressionModel()
    {
        Reset();
    }

    public void AddXP(int amount)
    {
        CurrentXP += amount;

        while (CurrentLevel - 1 < _xpThresholds.Length && CurrentXP >= CurrentThreshold)
        {
            CurrentXP -= CurrentThreshold;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        CurrentLevel++;
        CurrentThreshold = (CurrentLevel - 1 < _xpThresholds.Length) ? _xpThresholds[CurrentLevel - 1] : int.MaxValue;
    }

    public void UnlockAbility(MetaActiveAbility ability)
    {
        if (!UnlockedAbilities.Contains(ability))
        {
            UnlockedAbilities.Add(ability);
        }
    }

    public void Restore(int level, int xp, int threshold, IEnumerable<MetaActiveAbility> unlockedAbilities)
    {
        CurrentLevel = level;
        CurrentXP = xp;
        CurrentThreshold = threshold;
        UnlockedAbilities = new HashSet<MetaActiveAbility>(unlockedAbilities);
    }
    
    //Сброс прогресса (только мета, без влияния на забег).
    public void Reset()
    {
        CurrentXP = 0;
        CurrentLevel = 1;
        CurrentThreshold = _xpThresholds[0];
        UnlockedAbilities.Clear();
    }
}
