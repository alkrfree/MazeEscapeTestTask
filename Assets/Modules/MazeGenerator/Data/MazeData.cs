using Modules.MazeGenerator.Scripts;
using UnityEngine;

namespace Modules.MazeGenerator.Data
{
  [CreateAssetMenu(fileName = "MazeData", menuName = "MazeGenerator/MazeData", order = 0)]
  public class MazeData : ScriptableObject
  {
    [SerializeField] [Range(1, 30)] public int MazeSizeX;
    [SerializeField] [Range(1, 30)] public int MazeSizeY;
  }
}