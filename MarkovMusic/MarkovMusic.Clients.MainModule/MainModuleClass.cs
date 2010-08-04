using CompositApplication;
using MarkovMusic.Clients.MainModule.MusicGrid;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;

namespace MarkovMusic.Clients.MainModule
{
    public class MainModuleClass : IMainModule
    {
        private readonly IUnityContainer _unityContainer;
        private readonly IRegionManager _regionManager;

        public MainModuleClass(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _unityContainer = unityContainer;
            _unityContainer.RegisterType(typeof (IMusicGridPresenter), typeof (MusicGridPresenter), false);
            _unityContainer.RegisterType(typeof(IMusicGridView), typeof(MusicGridView), false);
        }

        #region Implementation of IModule

        public void Initialize()
        {
            var musicGridPresenter = _unityContainer.Resolve<IMusicGridPresenter>();
            _regionManager.Regions[RegionNames.MiddleRegion].Add(musicGridPresenter.View);
            _regionManager.Regions[RegionNames.MiddleRegion].Activate(musicGridPresenter.View);
        }

        #endregion
    }
}