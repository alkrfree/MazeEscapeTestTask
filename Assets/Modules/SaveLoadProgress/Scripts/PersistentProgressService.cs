using Modules.SaveLoadProgress.Data;

namespace Modules.SaveLoadProgress.Scripts
{
  public class PersistentProgressService : IPersistentProgressService
  {
    public PlayerProgress Progress { get; set; }
  }
}