using System;
using CodeBase.Data;

namespace Modules.LoadProgress.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public PlayerStats PlayerStats;
    public int LastLevel;


    public PlayerProgress(int initialLevel)
    {
      LastLevel = initialLevel;
      PlayerStats = new PlayerStats();
     
    }
  }
}