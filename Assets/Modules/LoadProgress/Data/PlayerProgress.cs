using System;

namespace CodeBase.Data
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