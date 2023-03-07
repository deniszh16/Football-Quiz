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
        
        private IPersistentProgressService _persistentProgressService;
        
        public override void InstallBindings()
        {
            BindStaticData();
            BindPersistentProgress();
            BindSceneLoader();
            BindSaveLoadService();
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
            _persistentProgressService = new PersistentProgressService();
            Container.BindInstance(_persistentProgressService).AsSingle();
        }

        private void BindSceneLoader()
        {
            SceneLoaderService sceneLoader = Container.InstantiatePrefabForComponent<SceneLoaderService>(_sceneLoader);
            Container.Bind<ISceneLoaderService>().To<SceneLoaderService>().FromInstance(sceneLoader).AsSingle();
        }

        private void BindSaveLoadService()
        {
            SaveLoadService saveLoadService = new SaveLoadService();
            saveLoadService.Construct(_persistentProgressService);
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().FromInstance(saveLoadService);
        }
    }
}