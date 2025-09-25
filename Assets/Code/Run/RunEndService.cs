using Zenject;
using UnityEngine;

public class RunEndService : IInitializable
{
    private readonly PlayerStatsModel _player;
    private readonly DeckModel _deck;
    private readonly RunSessionModel _session;
    private readonly MetaProgressionService _meta;
    private readonly SaveLoadManager _saveManager;
    private readonly SceneService _sceneService;

    public RunEndService(
        PlayerStatsModel player,
        DeckModel deck,
        RunSessionModel session,
        MetaProgressionService meta,
        SaveLoadManager saveManager,
        SceneService sceneService)
    {
        _player = player;
        _deck = deck;
        _session = session;
        _meta = meta;
        _saveManager = saveManager;
        _sceneService = sceneService;
    }

    public void Initialize()
    {
        _player.OnPlayerDied += EndRun;
        _deck.OnDeckEmpty += EndRun;
    }

    private void EndRun()
    {
        Debug.Log("🏁 Run ended!");

        //Берём количество зачищенных комнат
        int roomsCleared = _session.RoomsCleared;
        int gainedXp = roomsCleared * 10;

        if (gainedXp > 0)
        {
            _meta.AddXP(gainedXp);
            Debug.Log($"[Meta] Gained {gainedXp} XP from {roomsCleared} cleared rooms");
        }
        
        _saveManager.ClearSave();
        _sceneService.LoadMenuScene();
    }
}