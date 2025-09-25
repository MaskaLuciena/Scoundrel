using System.Collections.Generic;
using UnityEngine;

public class DeckSaveLoader : ISaveLoader
{
    private readonly DeckModel _deck;
    private readonly CardLoader _cardLoader;

    public DeckSaveLoader(DeckModel deck, CardLoader cardLoader, SaveLoadManager saveManager)
    {
        _deck = deck;
        _cardLoader = cardLoader;

        //регистрируемся в менеджере
        saveManager.RegisterLoader(this);
    }

    public void SaveGame(GameContext context)
    {
        var remainingCards = _deck.GetRemainingCards();
        var data = new DeckData
        {
            CardIds = new List<string>(remainingCards.Count)
        };

        foreach (var card in remainingCards)
            data.CardIds.Add(card.Id);

        context.Repository.SetData(data);
    }

    public void LoadGame(GameContext context)
    {
        if (context.Repository.TryGetData<DeckData>(out var data))
        {
            var restored = new List<CardData>();
            foreach (var id in data.CardIds)
            {
                var card = _cardLoader.LoadById(id);
                if (card != null) restored.Add(card);
            }

            _deck.SetState(restored);
        }
    }
}