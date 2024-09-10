using Modules.LoadProgress.Data;

namespace Modules.LoadProgress.Scripts
{
  public interface ISavedProgress : ISavedProgressReader
  {
    void UpdateProgress(PlayerProgress progress);
  }
}