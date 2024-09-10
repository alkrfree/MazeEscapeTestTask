using Modules.SaveLoadProgress.Data;

namespace Modules.SaveLoadProgress.Scripts
{
  public interface ISavedProgressReader
  {
    void LoadProgress(PlayerProgress progress);
  }
}