﻿using System;
using Modules.MapGenerator.Data;
using UnityEngine;
using Zenject;
using Object = System.Object;

namespace Modules.MapGenerator.Scripts
{
  public class MazeTileFactory
  {
    private readonly MazeTileFactoryData _data;
    private readonly Transform _mazeParent;
    private readonly DiContainer _container;

    public MazeTileFactory(MazeTileFactoryData data, MazeParent mazeParent, DiContainer container)
    {
      _data = data;
      _mazeParent = mazeParent.transform;
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
          return _container.InstantiatePrefabForComponent<ITileView>(_data.mazeTileViewPrefab, _mazeParent);
        case MazeTileModel.TileType.Start:
          return _container.InstantiatePrefabForComponent<ITileView>(_data.mazeTileViewStartPrefab, _mazeParent);
        case MazeTileModel.TileType.Finish:
          return _container.InstantiatePrefabForComponent<ITileView>(_data.mazeTileViewFinishPrefab, _mazeParent);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}