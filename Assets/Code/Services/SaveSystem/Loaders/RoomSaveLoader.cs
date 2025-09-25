using System.Collections.Generic;
using UnityEngine;

public class RoomSaveLoader : ISaveLoader
{
    private readonly RoomModel _room;
    private readonly CardLoader _cardLoader;

    public RoomSaveLoader(RoomModel room, CardLoader cardLoader, SaveLoadManager saveManager)
    {
        _room = room;
        _cardLoader = cardLoader;

        saveManager.RegisterLoader(this);
    }

    public void SaveGame(GameContext context)
    {
        var currentCards = _room.GetCards();
        var data = new RoomData
        {
            ActiveCardIds = new List<string>(currentCards.Count)
        };

        foreach (var card in currentCards)
            data.ActiveCardIds.Add(card.Id);

        context.Repository.SetData(data);
    }

    public void LoadGame(GameContext context)
    {
        if (context.Repository.TryGetData<RoomData>(out var data))
        {
            var restored = new List<CardData>();
            foreach (var id in data.ActiveCardIds)
            {
                var card = _cardLoader.LoadById(id);
                if (card != null) restored.Add(card);
            }

            _room.SetState(restored);
        }
    }
}