using Modules.LoadProgress.Data;

namespace Modules.LoadProgress.Scripts
{
  public interface ISavedProgressReader
  {
    void LoadProgress(PlayerProgress progress);
  }
}