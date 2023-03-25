using Code.Services.Ads;
using Code.Services.Analytics;
using Code.Services.GooglePlay;
using Code.Services.MigrationOldProgress;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.Services.SceneLoader;
using Code.Services.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Bootstraper
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoaderService _sceneLoader;
        
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
            IAdService adService = new AdService(_progressService);
            Container.BindInstance(adService).AsSingle();
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
    }
}