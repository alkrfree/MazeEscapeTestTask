using Modules.MapGenerator.Scripts;
using UnityEngine;

namespace Modules.MapGenerator.Data
{
  [CreateAssetMenu(fileName = "MazeTileFactoryAttributes", menuName = "MazeGenerator/MazeTileFactoryAttributes", order = 1)]
  public class MazeTileFactoryData : ScriptableObject
  {
    public MazeTileView mazeTileViewPrefab;
    public MazeTileView mazeTileViewStartPrefab;
    public MazeTileView mazeTileViewFinishPrefab;
    public Vector2 TileSize; // refactor
  }
}