using System;
using System.Collections.Generic;
using Modules.LevelGenerator.Scripts;

namespace Modules.LevelGenerator.Data
{
  
  [Serializable]
  public class LevelSerializedData
  {
    public int LevelNum;
    public List<EnemySerializedData> Enemies;
    public List<TileSerializedData> Tiles;
  }
}