using Modules.LoadProgress.Data;

namespace Modules.LoadProgress.Scripts
{
  public class PersistentProgressService : IPersistentProgressService
  {
    public PlayerProgress Progress { get; set; }
  }
}