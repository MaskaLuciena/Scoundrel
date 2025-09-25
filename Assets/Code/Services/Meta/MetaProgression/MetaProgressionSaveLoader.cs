using UnityEngine;
using System.Collections.Generic;

public class MetaProgressionSaveLoader : ISaveLoader
{
    private readonly MetaProgressionModel _model;
    private readonly MetaAbilitiesConfig _config;

    public MetaProgressionSaveLoader(
        MetaProgressionModel model,
        MetaAbilitiesConfig config,
        SaveLoadManager saveLoadManager)
    {
        _model = model;
        _config = config;
        saveLoadManager.RegisterLoader(this);
    }

    public void SaveGame(GameContext context)
    {
        var data = new MetaProgressionSaveData
        {
            CurrentLevel = _model.CurrentLevel,
            CurrentXP = _model.CurrentXP,
            CurrentThreshold = _model.CurrentThreshold,
            UnlockedAbilityIds = new List<string>()
        };

        foreach (var ability in _model.UnlockedAbilities)
        {
            data.UnlockedAbilityIds.Add(ability.Id);
        }

        context.Repository.SetData(data);
    }

    public void LoadGame(GameContext context)
    {
        if (context.Repository.TryGetData<MetaProgressionSaveData>(out var data))
        {
            // Восстанавливаем активки по ID через конфиг
            var restoredAbilities = new List<MetaActiveAbility>();
            foreach (var abilityId in data.UnlockedAbilityIds)
            {
                var ability = FindAbilityById(abilityId);
                if (ability != null)
                    restoredAbilities.Add(ability);
            }

            _model.Restore(
                data.CurrentLevel,
                data.CurrentXP,
                data.CurrentThreshold,
                restoredAbilities
            );
        }
    }

    private MetaActiveAbility FindAbilityById(string id)
    {
        foreach (var entry in _config.GetAllAbilities())
        {
            if (entry.Id == id)
                return entry;
        }
        return null;
    }
}
