using CodeBase.Infrastructure;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using CodeBase.Services.Ads;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Randomizer;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
  public GameBootstrapper BootstrapperPrefab;
  public LoadingCurtain CurtainPrefab;

  public override void InstallBindings()
  {
    Container.Bind<IPersistentProgressService>().To<PersistentProgressService>().AsSingle();
    Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
    Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
    Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
    Container.Bind<IRandomService>().To<RandomService>().AsSingle();
    Container.Bind<IWindowService>().To<WindowService>().AsSingle();
    Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
    Container.Bind<IAdsService>().To<AdsService>().AsSingle();
    Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
    Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(CurtainPrefab).AsSingle().NonLazy();
    Container.Bind<SceneLoader>().AsSingle();
    Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
    Container.Bind<IInputService>().FromInstance(InputService()).AsSingle();
    Container.Bind<ICoroutineRunner>().To<GameBootstrapper>().FromComponentInNewPrefab(BootstrapperPrefab).AsSingle().NonLazy();
    
    Debug.Log("ProjectInstaller");
  }

  private static IInputService InputService() =>
    Application.isEditor
      ? (IInputService)new StandaloneInputService()
      : new MobileInputService();


}