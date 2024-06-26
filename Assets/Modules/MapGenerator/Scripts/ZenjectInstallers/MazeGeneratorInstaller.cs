using Modules.MapGenerator.Data;
using Modules.MapGenerator.Scripts;
using UnityEngine;
using Zenject;

public class MazeGeneratorInstaller : MonoInstaller
{
  [SerializeField] private MazeTileFactoryAttributes _mazeTileFactoryAttributes;
  [SerializeField] private Transform _mazeParent;

  public override void InstallBindings()
  {
    BindMazeTileFactory();
  }

  private void BindMazeTileFactory()
  {
    Container.Bind<MazeTileFactory>().FromNew().AsSingle().WithArguments(_mazeTileFactoryAttributes, _mazeParent);
  }
}