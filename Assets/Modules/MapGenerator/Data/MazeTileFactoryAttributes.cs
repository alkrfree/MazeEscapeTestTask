using Modules.MapGenerator.Scripts;
using UnityEngine;

namespace Modules.MapGenerator.Data
{
  [CreateAssetMenu(fileName = "MazeTileFactoryAttributes", menuName = "MazeGenerator/MazeTileFactoryAttributes", order = 0)]
  public class MazeTileFactoryAttributes : ScriptableObject
  {
    public MazeTileView MazeTileViewPrefab;
    public MazeTileView MazeTileViewStartPrefab;
    public MazeTileView MazeTileViewFinishPrefab;
    public Vector2 TileSize;
  }
}