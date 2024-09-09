using UnityEngine;

namespace Modules.MapGenerator.Scripts
{
  public interface ITileModel
  {
    public TileCoords TileCoords { get; }
    public bool IsVisited { get; set; }
    public MazeTileModel.TileType Type { get; set; }
    public int TilesFromStart { get; set; }
    public MazeTileModel.Direction CurrentDisabledWalls { get; set; }
  }
}