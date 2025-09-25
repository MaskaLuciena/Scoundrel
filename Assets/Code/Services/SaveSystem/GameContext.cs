public class GameContext
{
    public IGameRepository Repository { get; }

    public GameContext(IGameRepository repository)
    {
        Repository = repository;
    }
}