using Modules.LoadProgress.Data;

namespace Modules.LoadProgress.Scripts
{
  public interface ISaveLoadService 
  {
    void SaveProgress();
    PlayerProgress LoadProgress();
  }
}