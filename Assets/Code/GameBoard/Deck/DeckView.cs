using TMPro;
using UnityEngine;
using Zenject;

public class DeckView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deckText;

    private DeckModel _model;

    [Inject]
    public void Initialize(DeckModel model)
    {
        _model = model;
        _model.OnDeckCountChanged += UpdateDeckText;
    }
    private void OnDestroy()
    {
        if (_model != null)
        {
            _model.OnDeckCountChanged -= UpdateDeckText;
        }
    }
    private void UpdateDeckText(int count)
    {
        deckText.text = count.ToString();
    }
}