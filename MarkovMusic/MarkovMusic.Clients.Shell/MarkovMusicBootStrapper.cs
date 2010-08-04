using CompositApplication;
using MarkovMusic.Clients.MainModule;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;

namespace MarkovMusic.Clients.Shell
{
    public class MarkovMusicBootstrapper : Bootstrapper
    {
        protected override IModuleCatalog GetModuleCatalog()
        {
            return ((ModuleCatalog) base.GetModuleCatalog()).AddModule(typeof (IMainModule));
        }
        protected override IUnityContainer CreateContainer()
        {
            var unityContainer = base.CreateContainer();
            unityContainer.RegisterType(typeof (IMainModule), typeof (MainModuleClass), true);
            return unityContainer;
        }
    }
}