using Code.Services.Ads;
using Code.Services.GooglePlay;
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
            BindAdService();
            BindGooglePlayService();
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
            _saveLoadService = new SaveLoadService();
            _saveLoadService.Construct(_progressService);
            Container.BindInstance(_saveLoadService);
        }

        private void BindAdService()
        {
            IAdService adService = new AdService();
            adService.Construct(_progressService, _saveLoadService);
            Container.BindInstance(adService).AsSingle();
        }

        private void BindGooglePlayService()
        {
            IGooglePlayService googlePlayService = new GooglePlayService();
            googlePlayService.ActivateService();
            Container.BindInstance(googlePlayService).AsSingle();
        }
    }
}