public interface ISaveLoader
{
    void SaveGame(GameContext context);
    void LoadGame(GameContext context);
}