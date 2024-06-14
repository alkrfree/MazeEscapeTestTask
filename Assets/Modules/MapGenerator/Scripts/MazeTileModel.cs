using System;
using UnityEngine;

namespace Modules.MapGenerator.Scripts
{
  public class MazeTileModel
  {

    public event Action<Direction> DisableWallsDirection;

    [Flags]
    public enum Direction
    {
      None = 0,
      Up = 1 << 0, // 1
      Down = 1 << 1, // 2
      Right = 1 << 2, // 4
      Left = 1 << 3 // 8
    }

    public Vector2Int TileCoords { get; }
    public bool IsDeadEnd { get; set; }
    public bool IsVisited { get; set; }

    private Direction _currentDisabledWalls;
    
    
    public MazeTileModel(Vector2Int _tileCoords)
    {
      TileCoords = _tileCoords;
    }
    
    public void SetWallVisibility(Direction wallDirection)
    {
      _currentDisabledWalls |= wallDirection;
      DisableWallsDirection?.Invoke(_currentDisabledWalls);
    }

  }
}