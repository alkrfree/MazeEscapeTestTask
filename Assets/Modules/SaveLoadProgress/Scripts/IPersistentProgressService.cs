using CodeBase.Services;
using Modules.SaveLoadProgress.Data;

namespace Modules.SaveLoadProgress.Scripts
{
  public interface IPersistentProgressService 
  {
    PlayerProgress Progress { get; set; }
  }
}