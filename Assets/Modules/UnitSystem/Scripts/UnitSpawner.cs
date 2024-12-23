using Zenject;

namespace Modules.UnitSystem.Scripts
{
  public class UnitSpawner : IInitializable
  {
    private UnitFactory _unitFactory;
    
    [Inject]
    private void Construct(UnitFactory unitFactory)
    {
      _unitFactory = unitFactory;
    }

    public void Initialize()
    {
      _unitFactory.Spawn();
    }
  }
}
