using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
using CodeBase.StaticData;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initial";
        private const string MainScene = "Main";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }

        public void Enter() => 
            _sceneLoader.Load(InitialScene, onLoaded: EnterLoadLevel);

        public void Exit()
        {
            
        }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadLevelState, string>(MainScene);

        private void RegisterServices()
        {
            RegisterStaticData();
            
            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>(), _services.Single<IStaticDataService>()));
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadResources();
            _services.RegisterSingle(staticData);
        }

        private static IInputService InputService() => 
            new MobileInputService();
    }
}