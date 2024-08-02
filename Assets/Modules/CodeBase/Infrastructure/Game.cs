using CodeBase.Infrastructure.States;
using Zenject;

namespace CodeBase.Infrastructure
{
  public class Game
  {
    public GameStateMachine StateMachine;

    public Game(DiContainer diContainer)
    {
      StateMachine = diContainer.Instantiate<GameStateMachine>();
    }
  }
}