using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SceneService>().AsSingle().NonLazy();
        Container.Bind<IGameRepository>().To<GameRepository>().AsSingle();
        Container.Bind<SaveLoadManager>().AsSingle().NonLazy();
        Container.Bind<CardLoader>().AsSingle().WithArguments("Cards/Generated");
    }
}