using System.Threading.Tasks;
using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using CodeBase.UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private LoadingCurtain _loadingCurtain;
    private IGameFactory _gameFactory;
    private IPersistentProgressService _progressService;
    private IStaticDataService _staticData;
    private IUIFactory _uiFactory;

    public LoadLevelState(
      IGameStateMachine gameStateMachine,
      SceneLoader sceneLoader,
      LoadingCurtain loadingCurtain,
      IGameFactory gameFactory,
      IPersistentProgressService progressService,
      IStaticDataService staticDataService,
      IUIFactory uiFactory)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
      _gameFactory = gameFactory;
      _progressService = progressService;
      _staticData = staticDataService;
      _uiFactory = uiFactory;
    }

    public void Enter(string sceneName)
    {
      _loadingCurtain.Show();
      _gameFactory.Cleanup();
      _gameFactory.WarmUp();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() =>
      _loadingCurtain.Hide();

    private async void OnLoaded()
    {
      await InitUIRoot();
      await InitGameWorld();
      InformProgressReaders();

      _stateMachine.Enter<GameLoopState>();
    }

    private async Task InitUIRoot() =>
      await _uiFactory.CreateUIRoot();

    private void InformProgressReaders()
    {
      foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
        progressReader.LoadProgress(_progressService.Progress);
    }

    private async Task InitGameWorld()
    {
     // LevelStaticData levelData = LevelStaticData();

   //   await InitSpawners(levelData);
      //   await InitLootPieces();
     // GameObject hero = await InitHero(levelData);
     // await InitLevelTransfer(levelData);
    //  await InitHud(hero);
   //   CameraFollow(hero); // TODO
    }

    private async Task InitSpawners(LevelStaticData levelStaticData)
    {
      foreach (EnemySpawnerStaticData spawnerData in levelStaticData.EnemySpawners)
        await _gameFactory.CreateSpawner(spawnerData.Id, spawnerData.Position, spawnerData.MonsterTypeId);
    }

    private async Task<GameObject> InitHero(LevelStaticData levelStaticData) =>
      await _gameFactory.CreateHero(levelStaticData.InitialHeroPosition);

    private async Task InitLevelTransfer(LevelStaticData levelData) =>
      await _gameFactory.CreateLevelTransfer(levelData.LevelTransfer.Position);

    private async Task InitHud(GameObject hero)
    {
      GameObject hud = await _gameFactory.CreateHud();
    }

    private LevelStaticData LevelStaticData() =>
      _staticData.ForLevel(SceneManager.GetActiveScene().name);

    private void CameraFollow(GameObject hero) =>
      Camera.main.GetComponent<CameraFollow>().Follow(hero);
  }
}