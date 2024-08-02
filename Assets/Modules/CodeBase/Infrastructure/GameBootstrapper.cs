using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
  {
    private Game _game;

    private DiContainer _diContainer;

    [Inject]
    private void Construct(DiContainer diContainer)
    {
      _diContainer = diContainer;
    }

    private void Awake()
    {
      _game = new Game(_diContainer);
      _game.StateMachine.Enter<BootstrapState>();

      DontDestroyOnLoad(this);
    }
  }
}