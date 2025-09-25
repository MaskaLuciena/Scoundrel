using UnityEngine;
using TMPro;
using Zenject;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private RectTransform weaponSlot;
    [SerializeField] private CardView cardPrefab;
    [SerializeField] private TextMeshProUGUI thresholdText;

    private PlayerStatsModel _model;
    private CardView _currentWeaponView;

    [Inject]
    public void Initialize(PlayerStatsModel model)
    {
        _model = model;
        _model.OnHPChanged += UpdateHP;
        _model.OnWeaponChanged += UpdateWeapon;
        _model.OnWeaponThresholdChanged += UpdateThreshold;

        UpdateHP(_model.CurrentHP);
        UpdateWeapon(_model.CurrentWeapon);
    }

    private void OnDestroy()
    {
        if (_model != null)
        {
            _model.OnHPChanged -= UpdateHP;
            _model.OnWeaponChanged -= UpdateWeapon;
            _model.OnWeaponThresholdChanged -= UpdateThreshold;
        }
    }

    private void UpdateHP(int newHP)
    {
        hpText.text = $"{newHP}/20";
    }

    private void UpdateWeapon(CardData weapon)
    {
        // Удаляем старый CardView, если был
        if (_currentWeaponView != null)
            Destroy(_currentWeaponView.gameObject);

        if (weapon != null)
        {
            _currentWeaponView = Instantiate(cardPrefab, weaponSlot);
            _currentWeaponView.SetCard(weapon);

            //гарантируем, что карта уходит на "дно", а текст порога остаётся сверху
            _currentWeaponView.transform.SetSiblingIndex(0);
        }
        else
        {
            _currentWeaponView = null;
            thresholdText.text = "";
        }
    }
    
    private void UpdateThreshold(int newThreshold)
    {
        if (_model.CurrentWeapon != null)
            thresholdText.text = newThreshold.ToString();
    }
}