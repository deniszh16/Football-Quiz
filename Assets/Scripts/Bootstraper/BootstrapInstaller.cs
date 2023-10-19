using PimDeWitte.UnityMainThreadDispatcher;
using Services.Ads;
using Services.Analytics;
using Services.GooglePlay;
using Services.MigrationOldProgress;
using Services.PersistentProgress;
using Services.SaveLoad;
using Services.SceneLoader;
using Services.StaticData;
using UnityEngine;
using Zenject;

namespace Bootstraper
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoaderService _sceneLoader;
        [SerializeField] private UnityMainThreadDispatcher _mainThreadDispatcher;
        
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        
        public override void InstallBindings()
        {
            BindStaticData();
            BindPersistentProgress();
            BindSceneLoader();
            BindSaveLoadService();
            BindMigrationOldProgressService();
            BindAdService();
            BindGooglePlayService();
            BindFirebaseService();
            BindMainThreadDispatcher();
        }

        private void BindStaticData()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadQuestionsCountries();
            staticDataService.LoadQuestionsFacts();
            staticDataService.LoadQuestionsPlayers();
            staticDataService.LoadLegends();
            Container.BindInstance(staticDataService).AsSingle();
        }

        private void BindPersistentProgress()
        {
            _progressService = new PersistentProgressService();
            Container.BindInstance(_progressService).AsSingle();
        }

        private void BindSceneLoader()
        {
            SceneLoaderService sceneLoader = Container.InstantiatePrefabForComponent<SceneLoaderService>(_sceneLoader);
            Container.Bind<ISceneLoaderService>().To<SceneLoaderService>().FromInstance(sceneLoader).AsSingle();
        }

        private void BindSaveLoadService()
        {
            _saveLoadService = new SaveLoadService(_progressService);
            Container.BindInstance(_saveLoadService);
        }

        private void BindMigrationOldProgressService()
        {
            IMigrationOldProgressService migrationService =
                new MigrationOldProgressService(_progressService, _saveLoadService);
            Container.BindInstance(migrationService).AsSingle();
        }

        private void BindAdService()
        {
            IAdService adService = new AdService();
            Container.BindInstance(adService).AsSingle();
            adService.Initialization();
        }

        private void BindGooglePlayService()
        {
            IGooglePlayService googlePlayService = new GooglePlayService();
            googlePlayService.ActivateService();
            Container.BindInstance(googlePlayService).AsSingle();
        }

        private void BindFirebaseService()
        {
            IFirebaseService firebaseService = new FirebaseService();
            firebaseService.Initialization();
            Container.BindInstance(firebaseService).AsSingle();
        }

        private void BindMainThreadDispatcher()
        {
            UnityMainThreadDispatcher unityMainThreadDispatcher =
                Container.InstantiatePrefabForComponent<UnityMainThreadDispatcher>(_mainThreadDispatcher); 
            Container.BindInstance(unityMainThreadDispatcher).AsSingle();
        }
    }
}