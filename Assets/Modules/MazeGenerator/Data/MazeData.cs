using Modules.MapGenerator.Scripts;
using UnityEngine;

namespace Modules.MapGenerator.Data
{
  [CreateAssetMenu(fileName = "MazeData", menuName = "MazeGenerator/MazeData", order = 0)]
  public class MazeData : ScriptableObject
  {
    [SerializeField] [Range(1, 30)] public int MazeSizeX;
    [SerializeField] [Range(1, 30)] public int MazeSizeY;
  }
}