using UnityEngine;
using Zenject;

public class AbilityMenuView : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private AbilityIconView abilityIconPrefab;

    private MetaProgressionService _progression;

    [Inject]
    public void Construct(MetaProgressionService progression)
    {
        _progression = progression;
        _progression.OnProgressionChanged += Refresh;

        Refresh();
    }

    private void OnDestroy()
    {
        if (_progression != null)
            _progression.OnProgressionChanged -= Refresh;
    }

    private void Refresh()
    {
        if (container == null) return; // контейнер уже уничтожен

        // Сначала собираем список детей
        int childCount = container.childCount;
        Transform[] children = new Transform[childCount];
        for (int i = 0; i < childCount; i++)
            children[i] = container.GetChild(i);

        // Теперь удаляем
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i] != null)
                Destroy(children[i].gameObject);
        }

        // Спавним заново
        foreach (var ability in _progression.GetAllAbilities())
        {
            var icon = Instantiate(abilityIconPrefab, container);
            bool unlocked = _progression.IsAbilityUnlocked(ability);
            int requiredLevel = _progression.GetRequiredLevel(ability);

            icon.Setup(ability, unlocked, requiredLevel);
        }
    }

}