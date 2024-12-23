using UnityEngine;

namespace Modules.UnitSystem.Data
{
  [CreateAssetMenu(fileName = "UnitSpawnerData", menuName = "UnitSpawner/UnitSpawnerData", order = 0)]
  public class UnitSpawnerData : ScriptableObject
  {
    public int EnemyCount;
  }
}