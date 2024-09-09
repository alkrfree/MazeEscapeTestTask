using Modules.LevelGenerator.Scripts;
using Modules.MapGenerator.Data;
using Modules.MapGenerator.Scripts;
using UnityEngine;
using Zenject;

public class MazeGeneratorInstaller : MonoInstaller
{
  [SerializeField] private MazeTileFactoryData mazeTileFactoryData;
  [SerializeField] private GameObject _mazeParent;

  public override void InstallBindings()
  {
    BindMazeGenerator();
    BindMazeTileFactories();
    RegisterLevelLoader();
    Debug.Log("MazeGeneratorInstaller");

  }

  private void RegisterLevelLoader()
  {
    Container.Bind<ILevelLoader>().To<LevelLoader>().AsSingle();
    ILevelLoader levelLoader = Container.Resolve<ILevelLoader>();
    levelLoader.LoadLevelData();
  }

  private void BindMazeGenerator()
  {
    Container.Bind<MazeParent>().FromComponentInNewPrefab(_mazeParent).AsSingle().NonLazy();
  }

  private void BindMazeTileFactories()
  {
    Container.Bind<MazeTileFactoryData>().FromScriptableObject(mazeTileFactoryData).AsSingle();
    Container.Bind<MazeLoaderTileFactory>().FromNew().AsSingle();
  }

}