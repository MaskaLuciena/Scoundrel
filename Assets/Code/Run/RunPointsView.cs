using TMPro;
using UnityEngine;
using Zenject;

public class RunPointsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;

    private RunPointsService _service;

    [Inject]
    public void Construct(RunPointsService service)
    {
        _service = service;
        _service.OnRunPointsChanged += UpdatePoints;

        // сразу показать текущее значение
        UpdatePoints(_service.CurrentPoints);
    }

    private void OnDestroy()
    {
        if (_service != null)
            _service.OnRunPointsChanged -= UpdatePoints;
    }

    private void UpdatePoints(int newPoints)
    {
        pointsText.text = $"{newPoints} points";
    }
}