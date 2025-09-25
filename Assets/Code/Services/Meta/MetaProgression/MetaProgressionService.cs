using System;
using System.Collections.Generic;
using UnityEngine;

public class MetaProgressionService
{
    private readonly MetaProgressionModel _model;
    private readonly MetaAbilitiesConfig _config;

    public event Action OnProgressionChanged;

    public MetaProgressionService(MetaProgressionModel model, MetaAbilitiesConfig config)
    {
        _model = model;
        _config = config;

        InitializeAbilitiesForCurrentLevel();
    }

    public int CurrentLevel => _model.CurrentLevel;
    public int CurrentXP => _model.CurrentXP;
    public int CurrentThreshold => _model.CurrentThreshold;

    public void AddXP(int amount)
    {
        int oldLevel = _model.CurrentLevel;
        _model.AddXP(amount);

        if (_model.CurrentLevel > oldLevel)
        {
            UnlockAbilitiesForLevel(_model.CurrentLevel);
        }

        OnProgressionChanged?.Invoke();
    }

    private void UnlockAbilitiesForLevel(int level)
    {
        var newAbilities = _config.GetAbilitiesForLevel(level);
        foreach (var ability in newAbilities)
        {
            _model.UnlockAbility(ability);
        }

        OnProgressionChanged?.Invoke();
    }

    public IReadOnlyCollection<MetaActiveAbility> GetUnlockedAbilities()
        => _model.UnlockedAbilities;

    public IEnumerable<MetaActiveAbility> GetAllAbilities()
        => _config.GetAllAbilities();

    public bool IsAbilityUnlocked(MetaActiveAbility ability)
        => _model.UnlockedAbilities.Contains(ability);

    public int GetRequiredLevel(MetaActiveAbility ability)
    {
        foreach (var entry in _config.UnlocksByLevel)
        {
            if (entry.UnlockedAbilities.Contains(ability))
                return entry.Level;
        }
        return -1;
    }

    private void InitializeAbilitiesForCurrentLevel()
    {
        for (int lvl = 1; lvl <= _model.CurrentLevel; lvl++)
        {
            var abilities = _config.GetAbilitiesForLevel(lvl);
            foreach (var ability in abilities)
            {
                _model.UnlockAbility(ability);
            }
        }

        OnProgressionChanged?.Invoke();
    }

    //Сброс прогресса (XP, уровень, способности)
    public void ResetProgression()
    {
        _model.Reset();
        OnProgressionChanged?.Invoke();
    }
    
}
