public interface IGameRepository
{
    bool TryGetData<T>(out T data);
    void SetData<T>(T data);

    void SaveState();
    void LoadState();
}