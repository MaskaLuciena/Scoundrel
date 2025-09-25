using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RoomView : MonoBehaviour
{
    [SerializeField] private CardView[] cardSlots;
    [SerializeField] private AttackChoiceView attackChoiceView;

    public event Action<CardView, CardData> OnCardChosen;

    private RoomModel _model;

    [Inject]
    public void Initialize(RoomModel model)
    {
        _model = model;
        _model.OnRoomCardsChanged += ShowCards;

        // сразу показать стартовые карты (если есть)
        ShowCards(new List<CardData>(_model.CurrentCards));
    }
    public void ShowEnemyChoice(CardData enemy, Action onUseWeapon, Action onTakeDamage)
    {
        attackChoiceView.Show(onUseWeapon, onTakeDamage);
    }

    private void Awake()
    {
        foreach (var cardSlot in cardSlots)
            cardSlot.OnCardClicked += HandleCardClicked;
    }

    private HashSet<CardData> _shownCards = new HashSet<CardData>();

    public void ShowCards(List<CardData> cards)
    {
        for (int i = 0; i < cardSlots.Length; i++)
        {
            if (i < cards.Count)
            {
                cardSlots[i].gameObject.SetActive(true);
                cardSlots[i].SetCard(cards[i]);

                // Анимация только если карта новая
                if (!_shownCards.Contains(cards[i]))
                {
                    var animator = cardSlots[i].GetComponent<CardAnimator>();
                    if (animator != null)
                        animator.ScaleIn();

                    _shownCards.Add(cards[i]);
                }
            }
            else
            {
                cardSlots[i].gameObject.SetActive(false);
            }
        }
    }


    
    public int ActiveCardCount()
    {
        int count = 0;
        for (int i = 0; i < cardSlots.Length; i++)
            if (cardSlots[i].gameObject.activeSelf) count++;
        return count;
    }

    private void HandleCardClicked(CardView view, CardData data)
    {
        OnCardChosen?.Invoke(view, data);
    }

}
