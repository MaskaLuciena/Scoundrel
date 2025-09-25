using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller
{
    [Header("Configs")]
    [SerializeField] private PlayerConfig _playerConfig;

    [Header("Player")]
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerController _playerController;

    [Header("Deck & Room")]
    [SerializeField] private DeckView _deckView;
    [SerializeField] private RoomView _roomView;

    [Header("Abilities UI")]
    [SerializeField] private AbilityRunView _abilityRunView;

    public override void InstallBindings()
    {
        BindPlayer();
        BindDeckAndRoom();
        BindSaveLoaders();
        BindRunSession();
        BindGameFlow();
        BindMeta();
    }

    private void BindPlayer()
    {
        Container.Bind<PlayerStatsModel>()
            .AsSingle()
            .WithArguments(_playerConfig.MaxHP);

        Container.BindInstance(_playerView).AsSingle();
        Container.BindInstance(_playerController).AsSingle();
    }

    private void BindDeckAndRoom()
    {
        Container.Bind<DeckModel>().AsSingle();
        Container.BindInstance(_deckView).AsSingle();

        Container.Bind<RoomModel>().AsSingle();
        Container.BindInstance(_roomView).AsSingle();
    }

    private void BindSaveLoaders()
    {
        Container.Bind<PlayerSaveLoader>().AsSingle().NonLazy();
        Container.Bind<DeckSaveLoader>().AsSingle().NonLazy();
        Container.Bind<RoomSaveLoader>().AsSingle().NonLazy();
    }

    private void BindRunSession()
    {
        Container.Bind<RunSessionModel>().AsSingle();
        Container.Bind<RunPointsService>().AsSingle();
    }

    private void BindGameFlow()
    {
        Container.Bind<IInitializable>().To<GameInitializer>().AsSingle().NonLazy();
        Container.Bind<IInitializable>().To<RunEndService>().AsSingle().NonLazy();

        Container.Bind<RoomFlowHandler>().AsSingle();
        Container.Bind<EscapeHandler>().AsSingle();

        // все хендлеры карт
        Container.Bind<ICardHandler>().To<WeaponCardHandler>().AsSingle();
        Container.Bind<ICardHandler>().To<HealCardHandler>().AsSingle();
        Container.Bind<ICardHandler>().To<EnemyCardHandler>().AsSingle();
    }

    private void BindMeta()
    {
        Container.Bind<AbilityService>().AsSingle();
        Container.BindInstance(_abilityRunView).AsSingle();
    }
}
