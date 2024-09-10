using CodeBase.Data;
using Modules.LoadProgress.Data;
using Modules.LoadProgress.Scripts;
using UnityEngine;

namespace CodeBase.Services.SaveLoad
{
  public class SaveLoadService : ISaveLoadService
  {
    private const string ProgressKey = "Progress";
    
    private readonly IPersistentProgressService _progressService;
   // private readonly IGameFactory _gameFactory;

    public SaveLoadService(IPersistentProgressService progressService)
    {
      _progressService = progressService;
      //_gameFactory = gameFactory;
    }

    public void SaveProgress()
    {
      /*foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
        progressWriter.UpdateProgress(_progressService.Progress);*/
      
      PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
    }

    public PlayerProgress LoadProgress()
    {
      return PlayerPrefs.GetString(ProgressKey)?
        .ToDeserialized<PlayerProgress>();
    }
  }
}