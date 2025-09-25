using Zenject;
using UnityEngine;

public class GameInitializer : IInitializable
{
    private readonly SaveLoadManager _saveManager;
    private readonly PlayerController _playerController;
    private readonly RoomModel _roomModel;
    private readonly RunSessionModel _runSession;

    public GameInitializer(
        SaveLoadManager saveManager,
        PlayerController playerController,
        RoomModel roomModel,
        RunSessionModel runSession)
    {
        _saveManager = saveManager;
        _playerController = playerController;
        _roomModel = roomModel;
        _runSession = runSession;
    }

    public void Initialize()
    {
        //всегда чистим сессию при старте
        _runSession.Reset();

        if (_saveManager.LoadFromSave)
        {
            Debug.Log("📂 Загружаем сохранение...");
            _saveManager.LoadGame();

            _playerController.RestoreFromSave(_roomModel);
        }
        else
        {
            Debug.Log("🆕 Старт новой игры...");
            _playerController.StartNewGame();
        }
    }
}