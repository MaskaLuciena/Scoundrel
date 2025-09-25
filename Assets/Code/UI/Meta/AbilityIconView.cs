using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class AbilityIconView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI refs")]
    [SerializeField] private Image iconImage;
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI nameText;
    public void Setup(MetaActiveAbility ability, bool unlocked, int requiredLevel)
    {
        iconImage.sprite = ability.Icon;
        iconImage.color = unlocked ? Color.white : new Color(1f, 1f, 1f, 0.4f);

        if (nameText != null)
        {
            nameText.text = ability.Name;
        }
        
        descriptionText.text = $"{ability.Description}\nСтоимость: {ability.Cost}";
        levelText.text = unlocked ? "" : $"Откроется на уровне {requiredLevel}";

        if (descriptionPanel != null)
        {
            descriptionPanel.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (descriptionPanel != null)
        {
            descriptionPanel.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (descriptionPanel != null)
        {
            descriptionPanel.SetActive(false);
        }
    }
}