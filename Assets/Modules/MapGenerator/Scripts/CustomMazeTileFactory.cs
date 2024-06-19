using Modules.MapGenerator.Data;
using UnityEngine;
using Zenject;

namespace Modules.MapGenerator.Scripts
{
  public class CustomMazeTileFactory : IFactory<MazeTileModel, MazeTileView>
  {
    private readonly MazeTileFactoryAttributes _attributes;
    private readonly Transform _parent;
    private readonly DiContainer _container;

    public CustomMazeTileFactory(MazeTileFactoryAttributes attributes, Transform parent, DiContainer container)
    {
      _attributes = attributes;
      _parent = parent;
      _container = container;
    }

    public MazeTileView Create(MazeTileModel tileModel)
    {
      MazeTileView tileView = _container.InstantiatePrefabForComponent<MazeTileView>(GetTilePrefab(tileModel), _parent);
      tileView.Init(tileModel);
      Vector2Int coords = tileModel.TileCoords;
      tileView.transform.localPosition = new Vector2(coords.x * _attributes.TileSize.x, coords.y * _attributes.TileSize.y);
      return tileView;
    }

    private MazeTileView GetTilePrefab(MazeTileModel tileModel)
    {
      MazeTileView prefab = _attributes.MazeTileViewPrefab;

      if (tileModel.IsStart)
        prefab = _attributes.MazeTileViewStartPrefab;
      else if (tileModel.IsFinish)
        prefab = _attributes.MazeTileViewFinishPrefab;
      return prefab;
    }
  }
}