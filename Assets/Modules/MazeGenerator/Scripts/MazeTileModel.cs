using System;
using Modules.LevelGenerator.Scripts;

namespace Modules.MapGenerator.Scripts
{
  public class MazeTileModel : ITileModel
  {
    [Flags]
    public enum Direction
    {
      None = 0,
      Up = 1 << 0,
      Down = 1 << 1,
      Right = 1 << 2,
      Left = 1 << 3
    }
    
    public enum TileType
    {
      Normal = 0,
      Start,
      Finish
    }
    public TileCoords TileCoords { get; }
    public bool IsVisited { get; set; }
    public TileType Type { get; set; }

    public int TilesFromStart { get; set; }

    private Direction _currentDisabledWalls;

    public Direction CurrentDisabledWalls
    {
      get => _currentDisabledWalls;
      set => _currentDisabledWalls |= value;
    }

    public MazeTileModel(int x, int y)
    {
      TileCoords = new TileCoords(x,y);
    }
    
    public MazeTileModel(TileSerializedData data)
    {
      TileCoords = new TileCoords(data.TileCoords.X,data.TileCoords.Y);
      Type = data.Type;
      _currentDisabledWalls = data.CurrentDisabledWalls;
    }
  }
}