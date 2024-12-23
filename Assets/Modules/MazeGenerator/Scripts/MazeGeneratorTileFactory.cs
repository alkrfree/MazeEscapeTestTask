using System;
using Modules.MazeGenerator.Data;
using Modules.MazeGenerator.Scripts;
using UnityEngine;

namespace Modules.MazeGenerator.Scripts
{
  public class MazeGeneratorTileFactory : MonoBehaviour
  {
    [SerializeField] private MazeTileFactoryData Data;
    [SerializeField] private Transform _mazeParent;

    public MazeTileView Spawn(ITileModel tileModel)
    {
      MazeTileView tileViewView = GetTileInstance(tileModel);
      tileViewView.Init(tileModel);
      return tileViewView;
    }

    private MazeTileView GetTileInstance(ITileModel tileModel)
    {
      switch (tileModel.Type)
      {
        case MazeTileModel.TileType.Normal:
          return Instantiate(Data.mazeTileViewPrefab, _mazeParent);
        case MazeTileModel.TileType.Start:
          return Instantiate(Data.mazeTileViewStartPrefab, _mazeParent);
        case MazeTileModel.TileType.Finish:
          return Instantiate(Data.mazeTileViewFinishPrefab, _mazeParent);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}