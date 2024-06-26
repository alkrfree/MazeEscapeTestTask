﻿using UnityEngine;

namespace Modules.MapGenerator.Scripts
{
  public interface ITileModel
  {
    public Vector2Int TileCoords { get; }
    public bool IsVisited { get; set; }
    public MazeTileModel.TileType Tile { get; set; }
    public int TilesFromStart { get; set; }
    public MazeTileModel.Direction CurrentDisabledWalls { get; set; }
  }
}