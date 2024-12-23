using System;
using System.Collections.Generic;

namespace Modules.LevelLoader.Data
{
  
  [Serializable]
  public class LevelSerializedData
  {
    public int LevelNum;
    public List<EnemySerializedData> Enemies;
    public List<TileSerializedData> Tiles;
  }
}