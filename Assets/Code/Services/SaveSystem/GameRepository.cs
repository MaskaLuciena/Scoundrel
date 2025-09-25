using System.Collections.Generic;
using UnityEngine;

public class GameRepository : IGameRepository
{
    private readonly Dictionary<string, string> _gameState = new();

    public bool TryGetData<T>(out T data)
    {
        string key = typeof(T).Name;
        if (_gameState.TryGetValue(key, out var json))
        {
            data = JsonUtility.FromJson<T>(json);
            return true;
        }

        data = default;
        return false;
    }

    public void SetData<T>(T data)
    {
        string key = typeof(T).Name;
        string json = JsonUtility.ToJson(data);
        _gameState[key] = json;
    }

    public void SaveState()
    {
        foreach (var kv in _gameState)
        {
            PlayerPrefs.SetString(kv.Key, kv.Value);
        }

        // сохраняем список ключей, чтобы потом знать, что грузить
        PlayerPrefs.SetString("SaveKeys", string.Join(",", _gameState.Keys));
        PlayerPrefs.Save();
    }

    public void LoadState()
    {
        // если ключей нет значит сейва нет
        if (!PlayerPrefs.HasKey("SaveKeys"))
            return;

        var keys = PlayerPrefs.GetString("SaveKeys").Split(',');
        foreach (var key in keys)
        {
            if (PlayerPrefs.HasKey(key))
            {
                _gameState[key] = PlayerPrefs.GetString(key);
            }
        }
    }
}