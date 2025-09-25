using UnityEngine;
using System.Collections.Generic;

public class CardLoader
{
    private readonly string _path;
    private Dictionary<string, CardData> _cacheById;

    public CardLoader(string path)
    {
        _path = path;
        LoadAll();
    }

    // Загрузка всех карт из Resources
    public List<CardData> LoadAll()
    {
        CardData[] loaded = Resources.LoadAll<CardData>(_path);

        _cacheById = new Dictionary<string, CardData>();
        List<CardData> cards = new List<CardData>();

        for (int i = 0; i < loaded.Length; i++)
        {
            CardData card = loaded[i];

            if (!string.IsNullOrEmpty(card.Id))
            {
                if (!_cacheById.ContainsKey(card.Id))
                {
                    _cacheById.Add(card.Id, card);
                    cards.Add(card);
                }
                else
                {
                    Debug.LogWarning($"Дублирующийся Id карты: {card.Id} в {card.name}");
                }
            }
            else
            {
                Debug.LogWarning($"У карты {card.name} не задан Id");
            }
        }

        return cards;
    }

    public CardData LoadById(string id)
    {
        if (_cacheById == null)
        {
            LoadAll();
        }

        if (_cacheById.ContainsKey(id))
        {
            return _cacheById[id];
        }

        Debug.LogError($"Карта с Id {id} не найдена в {_path}");
        return null;
    }
}