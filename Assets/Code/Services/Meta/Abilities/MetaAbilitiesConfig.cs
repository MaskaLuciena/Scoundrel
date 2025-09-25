using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MetaAbilitiesConfig", menuName = "Meta/Abilities Config")]
public class MetaAbilitiesConfig : ScriptableObject
{
    [System.Serializable]
    public class LevelUnlock
    {
        public int Level;
        public List<MetaActiveAbility> UnlockedAbilities = new();
    }


    [SerializeField] private List<LevelUnlock> unlocksByLevel = new();

    /// <summary>Все уровни и их абилки (только чтение).</summary>
    public IReadOnlyList<LevelUnlock> UnlocksByLevel => unlocksByLevel;

    public IReadOnlyList<MetaActiveAbility> GetAbilitiesForLevel(int level)
    {
        foreach (var entry in unlocksByLevel)
        {
            if (entry.Level == level)
                return entry.UnlockedAbilities;
        }
        return new List<MetaActiveAbility>();
    }

    public IEnumerable<MetaActiveAbility> GetAllAbilities()
    {
        foreach (var entry in unlocksByLevel)
        {
            foreach (var ability in entry.UnlockedAbilities)
                yield return ability;
        }
    }
}