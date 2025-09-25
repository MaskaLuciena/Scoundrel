using UnityEngine;

public class PlayerSaveLoader : ISaveLoader
{
    private readonly PlayerStatsModel _player;
    private readonly CardLoader _cardLoader;
    private readonly SaveLoadManager _saveManager;

    public PlayerSaveLoader(PlayerStatsModel player, CardLoader cardLoader, SaveLoadManager saveManager)
    {
        _player = player;
        _cardLoader = cardLoader;
        _saveManager = saveManager;

        //регистрируемся
        _saveManager.RegisterLoader(this);
    }

    public void SaveGame(GameContext context)
    {
        var data = new PlayerData
        {
            CurrentHP = _player.CurrentHP,
            WeaponThreshold = _player.WeaponThreshold,
            WeaponId = _player.CurrentWeapon?.Id
        };

        context.Repository.SetData(data);
    }

    public void LoadGame(GameContext context)
    {
        if (context.Repository.TryGetData<PlayerData>(out var data))
        {
            CardData weapon = string.IsNullOrEmpty(data.WeaponId)
                ? null
                : _cardLoader.LoadById(data.WeaponId);

            _player.SetState(data.CurrentHP, data.WeaponThreshold, weapon);
        }
    }
}