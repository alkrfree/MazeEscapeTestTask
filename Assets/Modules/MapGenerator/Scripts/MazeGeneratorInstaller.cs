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
    Container.BindFactory<MazeTileModel, MazeTileView, MazeTileFactory>()
      .FromIFactory(b => b.To<CustomMazeTileFactory>().AsSingle().WithArguments(_mazeTileFactoryAttributes,_mazeParent));
  }
}