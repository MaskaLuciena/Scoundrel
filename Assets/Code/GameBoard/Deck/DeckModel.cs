using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DeckModel
{
    private Queue<CardData> _deck = new();
    private readonly CardLoader _loader;

    public event Action<int> OnDeckCountChanged;
    public event Action OnDeckEmpty;

    [Inject]
    public DeckModel(CardLoader loader)
    {
        _loader = loader;
        Shuffle(_loader.LoadAll());
    }

    private void Shuffle(List<CardData> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int rand = UnityEngine.Random.Range(i, cards.Count);
            (cards[i], cards[rand]) = (cards[rand], cards[i]);
        }
        _deck = new Queue<CardData>(cards);
        OnDeckCountChanged?.Invoke(_deck.Count);
    }

    public List<CardData> DrawCards(int count)
    {
        var drawn = new List<CardData>();
        for (int i = 0; i < count && _deck.Count > 0; i++)
            drawn.Add(_deck.Dequeue());

        OnDeckCountChanged?.Invoke(_deck.Count);

        if (_deck.Count == 0)
            OnDeckEmpty?.Invoke();

        return drawn;
    }

    public void PutBackBottom(List<CardData> cards)
    {
        foreach (var c in cards)
            _deck.Enqueue(c);

        OnDeckCountChanged?.Invoke(_deck.Count);
    }

    public void SetState(List<CardData> cards)
    {
        _deck = new Queue<CardData>(cards);
        OnDeckCountChanged?.Invoke(_deck.Count);

        if (_deck.Count == 0)
            OnDeckEmpty?.Invoke();
    }

    public List<CardData> GetRemainingCards()
    {
        return new List<CardData>(_deck);
    }

    public int RemainingCount => _deck.Count;
}