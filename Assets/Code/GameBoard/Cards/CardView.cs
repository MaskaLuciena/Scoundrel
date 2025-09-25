using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CardView : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image icon;
    [SerializeField] private Button button;

    private CardData cardData;
    
    public event Action<CardView, CardData> OnCardClicked;
    private void Awake()
    {
        button.onClick.AddListener(() => OnCardClicked?.Invoke(this, cardData));
    }

    public void SetCard(CardData data)
    {
        cardData = data;

        valueText.text = data.value.ToString();
        nameText.text = data.cardName;
        icon.sprite = data.icon;

        gameObject.SetActive(true);
    }
}