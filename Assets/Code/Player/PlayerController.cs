using UnityEngine;
using Zenject;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private RoomView roomView;
    [SerializeField] private PlayerView playerView;

    private PlayerStatsModel _statsModel;
    private DeckModel _deckModel;
    private SaveLoadManager _saveManager;
    private RoomModel _roomModel;
    private RunPointsService _runPointsService;

    private RoomFlowHandler _roomFlow;
    private EscapeHandler _escapeHandler;
    private Dictionary<CardType, ICardHandler> _handlers;

    [Inject]
    public void Construct(PlayerStatsModel statsModel, DeckModel deckModel,
                          SaveLoadManager saveManager, RoomModel roomModel,
                          RunPointsService runPointsService)
    {
        _statsModel = statsModel;
        _deckModel = deckModel;
        _saveManager = saveManager;
        _roomModel = roomModel;
        _runPointsService = runPointsService;

        InitializeServices();
        InitializeHandlers();
    }

    private void Start()
    {
        roomView.OnCardChosen += HandleCardChosen;
        playerView.Initialize(_statsModel);
        _roomModel.OnRoomCardsChanged += cards => roomView.ShowCards(cards);
    }

    public void StartNewGame() => _roomFlow.ShowNewRoom();

    private void HandleCardChosen(CardView view, CardData card)
    {
        if (_handlers.TryGetValue(card.type, out var handler))
        {
            handler.Handle(view, card);
            
            if (card.type != CardType.Enemy)
            {
                _roomFlow.AfterCardResolved(view, card);
            }
        }
    }

    public void EscapeRoom() => _escapeHandler.TryEscape();
    public void ForceEscapeRoom() => _escapeHandler.ForceEscape();

    public void RestoreFromSave(RoomModel roomModel)
    {
        _roomModel.SetState(roomModel.GetCards());
        playerView.Initialize(_statsModel);
    }

    private void InitializeServices()
    {
        _roomFlow = new RoomFlowHandler(_deckModel, _roomModel, _saveManager, _runPointsService, roomView);
        _escapeHandler = new EscapeHandler(_deckModel, _roomModel, _runPointsService, _saveManager, roomView);
    }

    private void InitializeHandlers()
    {
        _handlers = new Dictionary<CardType, ICardHandler>
        {
            { CardType.Weapon, new WeaponCardHandler(_statsModel) },
            { CardType.Heal,   new HealCardHandler(_statsModel, _roomModel) },
            { CardType.Enemy,  new EnemyCardHandler(_statsModel, _roomModel, roomView, _roomFlow) }
        };
    }
}
