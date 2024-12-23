using Modules.LevelLoader.Scripts;
using UnityEngine;
using Zenject;

namespace Modules.MazeGenerator.Scripts.ZenjectInstallers
{
  public class LevelLoaderInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      RegisterLevelLoader();
      Debug.Log("LevelLoaderInstaller");
    }

    private void RegisterLevelLoader()
    {
      Container.Bind<ILevelLoader>().To<LevelLoader.Scripts.LevelLoader>().AsSingle();
      ILevelLoader levelLoader = Container.Resolve<ILevelLoader>();
      levelLoader.LoadLevelData();
    }
  }
}