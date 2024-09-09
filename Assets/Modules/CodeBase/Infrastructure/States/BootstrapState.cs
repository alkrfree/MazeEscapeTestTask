using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class BootstrapState : IState
  {
    private const string Initial = "BootScene";
    private readonly IGameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;


    public BootstrapState(IGameStateMachine stateMachine, SceneLoader sceneLoader)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
    }


    public void Enter()
    {
      _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
    }

    public void Exit()
    {
    }

    private void EnterLoadLevel()
    {
      Debug.Log("Level Loaded");
      _stateMachine.Enter<LoadProgressState>();
    }
  }
}