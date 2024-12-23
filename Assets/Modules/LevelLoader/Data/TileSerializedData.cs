using System;
using Modules.MazeGenerator.Scripts;

namespace Modules.LevelLoader.Data
{
  [Serializable]
  public class TileSerializedData
  {
    public TileCoords TileCoords;

    public MazeTileModel.TileType Type;

    public MazeTileModel.Direction CurrentDisabledWalls;


    public TileSerializedData()
    {
    }

    public TileSerializedData(MazeTileModel model)
    {
      TileCoords = model.TileCoords;
      Type = model.Type;
      CurrentDisabledWalls = model.CurrentDisabledWalls;
    }
  }
}