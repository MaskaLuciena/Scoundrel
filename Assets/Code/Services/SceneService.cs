using UnityEngine.SceneManagement;

public class SceneService
{
    private readonly SaveLoadManager _saveManager;

    public SceneService(SaveLoadManager saveManager)
    {
        _saveManager = saveManager;
    }

    public void LoadGameScene(bool loadFromSave)
    {
        if (loadFromSave)
            _saveManager.SetLoadGame();
        else
            _saveManager.SetNewGame();

        SceneManager.LoadScene(2);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(1);
    }
}