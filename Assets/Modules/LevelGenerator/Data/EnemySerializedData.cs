using System;
using CodeBase.StaticData;
using Modules.MapGenerator.Scripts;

namespace Modules.LevelGenerator.Data
{
  [Serializable]
  public class EnemySerializedData
  {
    public string Id;
    public TileCoords TileCoords;
    public EnemyTypeId EnemyTypeId;
    public string AdditionalAction;
  }
}