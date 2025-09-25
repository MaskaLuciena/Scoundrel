using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    [SerializeField] private MetaProgressionMenuView _menuView;

    public override void InstallBindings()
    {
        Container.BindInstance(_menuView).AsSingle();
    }
    
}