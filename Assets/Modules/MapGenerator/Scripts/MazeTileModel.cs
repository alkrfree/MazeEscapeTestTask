using System;
using UnityEngine;

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
    public Vector2Int TileCoords { get; }
    public bool IsVisited { get; set; }
    public TileType Tile { get; set; }

    public int TilesFromStart { get; set; }

    private Direction _currentDisabledWalls;

    public Direction CurrentDisabledWalls
    {
      get => _currentDisabledWalls;
      set => _currentDisabledWalls |= value;
    }

    public MazeTileModel(Vector2Int _tileCoords)
    {
      TileCoords = _tileCoords;
    }
  }
}