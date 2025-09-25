using UnityEngine;
using Zenject;

public class AbilityRunView : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private GameObject abilityButtonPrefab;

    private MetaProgressionService _progression;
    private AbilityService _abilityService;
    private RunPointsService _runPoints;

    [Inject]
    public void Construct(MetaProgressionService progression, AbilityService abilityService, RunPointsService runPoints)
    {
        _progression = progression;
        _abilityService = abilityService;
        _runPoints = runPoints;

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
        foreach (Transform child in container)
            Destroy(child.gameObject);

        foreach (var ability in _progression.GetUnlockedAbilities())
        {
            var obj = Instantiate(abilityButtonPrefab, container);
            var view = obj.GetComponent<AbilityButtonView>();
            view.Setup(ability, _abilityService, _runPoints);
        }
    }
}