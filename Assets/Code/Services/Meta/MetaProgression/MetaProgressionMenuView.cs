using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;

public class MetaProgressionMenuView : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private Slider xpSlider;
    [SerializeField] private TextMeshProUGUI abilityDescriptionText;

    private MetaProgressionService _service;

    [Inject]
    public void Construct(MetaProgressionService service)
    {
        _service = service;

        //подписка на изменения прогрессии
        _service.OnProgressionChanged += Refresh;

        //моментальный апдейт при старте
        Refresh();
    }

    private void OnDestroy()
    {
        if (_service != null)
            _service.OnProgressionChanged -= Refresh;
    }

    private void Refresh()
    {
        if (_service == null) return;

        // Уровень
        levelText.text = $"Level {_service.CurrentLevel}";

        // XP
        xpText.text = $"{_service.CurrentXP}/{_service.CurrentThreshold} XP";

        // Слайдер
        xpSlider.minValue = 0;
        xpSlider.maxValue = _service.CurrentThreshold;
        xpSlider.value = _service.CurrentXP;
    }

    private void ShowAbilityDetails(MetaActiveAbility ability)
    {
        abilityDescriptionText.text =
            $"{ability.Name}\n\n{ability.Description}\nСтоимость: {ability.Cost}";
    }
}