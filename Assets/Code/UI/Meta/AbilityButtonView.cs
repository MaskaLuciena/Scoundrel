using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityButtonView : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Button button;

    private MetaActiveAbility _ability;
    private AbilityService _abilityService;
    private RunPointsService _runPoints;

    public void Setup(MetaActiveAbility ability, AbilityService abilityService, RunPointsService runPoints)
    {
        _ability = ability;
        _abilityService = abilityService;
        _runPoints = runPoints;

        // 🟢 Назначаем UI
        icon.sprite = ability.Icon;
        costText.text = ability.Cost.ToString();
        nameText.text = ability.Name;   // ← теперь выводится имя

        Refresh(_runPoints.CurrentPoints);

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);

        // подписка на изменение очков
        _runPoints.OnRunPointsChanged += Refresh;
    }

    private void OnDestroy()
    {
        if (_runPoints != null)
            _runPoints.OnRunPointsChanged -= Refresh;
    }

    private void Refresh(int currentPoints)
    {
        bool canUse = currentPoints >= _ability.Cost;
        button.interactable = canUse;
        icon.color = canUse ? Color.white : new Color(1, 1, 1, 0.4f);
    }

    private void OnClick()
    {
        if (_abilityService.TryUseAbility(_ability))
        {
            Refresh(_runPoints.CurrentPoints);
        }
    }
}