using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public PlayerData Player;
    public RoomData Room;
    public DeckData Deck;
}

[System.Serializable]
public class PlayerData
{
    public int CurrentHP;
    public string WeaponId;
    public int WeaponThreshold;
}


[System.Serializable]
public class RoomData
{
    public List<string> ActiveCardIds; // id карт, разложенных в комнате
}

[System.Serializable]
public class DeckData
{
    public List<string> CardIds; // id карт из ScriptableObject
}
