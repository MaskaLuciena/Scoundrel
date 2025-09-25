using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button skillsButton;
    [SerializeField] private Button tutorialButton;

    [Header("Panels")]
    [SerializeField] private GameObject skillsPanel; // 🟢 панель со скиллами

    private SceneService _sceneService;
    private SaveLoadManager _saveManager;

    [Inject]
    public void Construct(SceneService sceneService, SaveLoadManager saveManager)
    {
        _sceneService = sceneService;
        _saveManager = saveManager;
    }

    private void Awake()
    {
        newGameButton.onClick.AddListener(OnNewGame);
        loadGameButton.onClick.AddListener(OnLoadGame);
        skillsButton.onClick.AddListener(EnableSkillsPanel);
        tutorialButton.onClick.AddListener(() => Debug.Log("Tutorial TODO"));

        // чтобы при старте панель была выключена
        if (skillsPanel != null)
            skillsPanel.SetActive(false);
    }

    private void OnNewGame()
    {
        _saveManager.SetNewGame();
        _sceneService.LoadGameScene(false);
    }

    private void OnLoadGame()
    {
        _saveManager.SetLoadGame();
        _sceneService.LoadGameScene(true);
    }

    private void EnableSkillsPanel()
    {
        skillsPanel.SetActive(true);
    }
}