using Zenject;

namespace Modules.UnitSystem.Scripts.ZenjectInstallers
{
    public class UnitSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<UnitFactory>().FromNew().AsSingle();
        }
    }
}