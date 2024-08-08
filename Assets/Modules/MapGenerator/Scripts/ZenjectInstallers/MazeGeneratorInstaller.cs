using Modules.MapGenerator.Data;
using Modules.MapGenerator.Scripts;
using UnityEngine;
using Zenject;

public class MazeGeneratorInstaller : MonoInstaller
{
  [SerializeField] private MazeData MazeData;
  [SerializeField] private MazeTileFactoryData mazeTileFactoryData;
  [SerializeField] private GameObject _mazeParent;

  public override void InstallBindings()
  {
    BindMazeGenerator();
    BindMazeTileFactory();
    
    Debug.Log("MazeGeneratorInstaller");

  }

  private void BindMazeGenerator()
  {
    Container.Bind<MazeGenerator>().AsSingle();
    Container.Bind<MazeData>().FromScriptableObject(MazeData).AsSingle();
    Container.Bind<MazeParent>().FromComponentInNewPrefab(_mazeParent).AsSingle().NonLazy();
  }

  private void BindMazeTileFactory()
  {
    Container.Bind<MazeTileFactoryData>().FromScriptableObject(mazeTileFactoryData).AsSingle();
    Container.Bind<MazeTileFactory>().FromNew().AsSingle();
  }

}