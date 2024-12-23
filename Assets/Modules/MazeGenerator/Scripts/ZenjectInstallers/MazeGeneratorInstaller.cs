using Modules.MazeGenerator.Data;
using UnityEngine;
using Zenject;

namespace Modules.MazeGenerator.Scripts.ZenjectInstallers
{
  public class MazeGeneratorInstaller : MonoInstaller
  {
    [SerializeField] private MazeTileFactoryData mazeTileFactoryData;
    [SerializeField] private GameObject _mazeParent;

    public override void InstallBindings()
    {
      BindMazeGenerator();
      BindMazeTileFactories();
      Debug.Log("MazeGeneratorInstaller");
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
}