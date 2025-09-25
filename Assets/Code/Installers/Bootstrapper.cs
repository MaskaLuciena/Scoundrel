using UnityEngine;
using Zenject;

public class Bootstrapper : MonoBehaviour
{
    private SceneService _sceneService;
    private SaveLoadManager _saveManager;

    [Inject]
    public void Construct(SceneService sceneService, SaveLoadManager saveManager)
    {
        _sceneService = sceneService;
        _saveManager = saveManager;
    }

    private void Start()
    {
        _saveManager.LoadGame();
        _sceneService.LoadMenuScene();
    }
}