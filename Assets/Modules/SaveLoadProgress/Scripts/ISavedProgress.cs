using Modules.SaveLoadProgress.Data;

namespace Modules.SaveLoadProgress.Scripts
{
  public interface ISavedProgress : ISavedProgressReader
  {
    void UpdateProgress(PlayerProgress progress);
  }
}