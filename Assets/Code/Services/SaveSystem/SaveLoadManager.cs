using UnityEngine;
using System.Collections.Generic;

public class SaveLoadManager
{
    private readonly List<ISaveLoader> _saveLoaders = new();
    private readonly GameContext _context;

    public bool LoadFromSave { get; private set; }

    public SaveLoadManager(IGameRepository repository)
    {
        _context = new GameContext(repository);
    }

    public void RegisterLoader(ISaveLoader loader)
    {
        if (!_saveLoaders.Contains(loader))
            _saveLoaders.Add(loader);
    }

    public void SetNewGame() => LoadFromSave = false;
    public void SetLoadGame() => LoadFromSave = true;

    public void SaveGame()
    {
        foreach (var loader in _saveLoaders)
        {
            loader.SaveGame(_context);
        }
        _context.Repository.SaveState();
    }

    public void LoadGame()
    {
        _context.Repository.LoadState();

        foreach (var loader in _saveLoaders)
        {
            loader.LoadGame(_context);
        }
    }

    // Очистка только данных текущего забега
    public void ClearRunData()
    {
        PlayerPrefs.DeleteKey("PlayerState");
        PlayerPrefs.DeleteKey("DeckState");
        PlayerPrefs.DeleteKey("RoomState");
        PlayerPrefs.Save();
    }
}