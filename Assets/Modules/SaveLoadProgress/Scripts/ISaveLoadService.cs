using Modules.SaveLoadProgress.Data;

namespace Modules.SaveLoadProgress.Scripts
{
  public interface ISaveLoadService 
  {
    void SaveProgress();
    PlayerProgress LoadProgress();
  }
}