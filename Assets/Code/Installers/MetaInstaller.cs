using UnityEngine;
using Zenject;

public class MetaInstaller : MonoInstaller
{
    [SerializeField] private MetaAbilitiesConfig _abilitiesConfig;

    public override void InstallBindings()
    {
        Container.Bind<MetaProgressionModel>().AsSingle();
        Container.BindInstance(_abilitiesConfig).AsSingle();
        Container.Bind<MetaProgressionService>().AsSingle();
        Container.Bind<MetaProgressionSaveLoader>().AsSingle().NonLazy();
    }
}