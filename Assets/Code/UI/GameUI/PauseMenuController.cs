using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private Button exitToMenuButton;

    private SceneService _sceneService;

    [Inject]
    public void Construct(SceneService sceneService)
    {
        _sceneService = sceneService;
    }

    private void Awake()
    {
        exitToMenuButton.onClick.AddListener(OnExitToMenu);
    }

    private void OnExitToMenu()
    {
        _sceneService.LoadMenuScene();
    }
}