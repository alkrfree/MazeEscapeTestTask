using CodeBase.Services;
using Modules.LoadProgress.Data;

namespace Modules.LoadProgress.Scripts
{
  public interface IPersistentProgressService 
  {
    PlayerProgress Progress { get; set; }
  }
}