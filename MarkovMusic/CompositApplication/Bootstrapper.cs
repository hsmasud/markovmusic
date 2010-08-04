using System;
using System.Windows;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.UnityExtensions;
using Microsoft.Practices.Unity;

namespace CompositApplication
{
    public class Bootstrapper : UnityBootstrapper
    {

        #region Overrides of UnityBootstrapper

        protected override IModuleCatalog GetModuleCatalog()
        {
            return new ModuleCatalog();
        }
        protected override DependencyObject CreateShell()
        {
            return (DependencyObject) Container.Resolve<IShellPresenter>().View;
        }

        protected override IUnityContainer CreateContainer()
        {
            var unityContainer = base.CreateContainer();
            unityContainer.RegisterType(typeof(IShellView), typeof(Shell), true);
            unityContainer.RegisterType(typeof(IShellPresenter), typeof(ShellPresenter), true);
            return unityContainer;
        }

        #endregion
    }
}