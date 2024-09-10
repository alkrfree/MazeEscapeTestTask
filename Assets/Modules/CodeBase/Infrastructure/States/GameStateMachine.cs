using System;
using System.Collections.Generic;
using CodeBase.Logic;
/*using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;*/
using Modules.LevelGenerator.Scripts;
using Modules.SaveLoadProgress.Scripts;

namespace CodeBase.Infrastructure.States
{
  public class GameStateMachine : IGameStateMachine
  {
    private Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    public GameStateMachine(
      SceneLoader sceneLoader,
      LoadingCurtain loadingCurtain,
      IPersistentProgressService progressService,
      /*IStaticDataService staticDataService,
      IUIFactory uiFactory,*/
      ISaveLoadService saveLoadProgress,
      ILevelLoader levelLoader
    )
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
        [typeof(LoadProgressState)] = new LoadProgressState(this, progressService, saveLoadProgress),
        [typeof(LoadLevelState)] = new LoadLevelState(this,
          sceneLoader,
          loadingCurtain,
          progressService,
          /*staticDataService,
          uiFactory,*/
          levelLoader),

        [typeof(GameLoopState)] = new GameLoopState()
      };
    }

    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();

      TState state = GetState<TState>();
      _activeState = state;

      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState =>
      _states[typeof(TState)] as TState;
  }
}