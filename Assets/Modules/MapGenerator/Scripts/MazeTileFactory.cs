using System;
using Modules.MapGenerator.Data;
using UnityEngine;
using Zenject;

namespace Modules.MapGenerator.Scripts
{
  public class MazeTileFactory
  {
    private readonly MazeTileFactoryAttributes _attributes;
    private readonly Transform _parent;
    private readonly DiContainer _container;

    public MazeTileFactory(MazeTileFactoryAttributes attributes, Transform parent, DiContainer container)
    {
      _attributes = attributes;
      _parent = parent;
      _container = container;
    }

    public ITileView Spawn(ITileModel tileModel)
    {
      ITileView tileViewView = GetTileInstance(tileModel);
      tileViewView.Init(tileModel);
      return tileViewView;
    }

    private ITileView GetTileInstance(ITileModel tileModel)
    {
      switch (tileModel.Tile)
      {
        case MazeTileModel.TileType.Normal:
          return _container.InstantiatePrefabForComponent<ITileView>(_attributes.mazeTileViewPrefab, _parent);
        case MazeTileModel.TileType.Start:
          return _container.InstantiatePrefabForComponent<ITileView>(_attributes.mazeTileViewStartPrefab, _parent);
        case MazeTileModel.TileType.Finish:
          return _container.InstantiatePrefabForComponent<ITileView>(_attributes.mazeTileViewFinishPrefab, _parent);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}