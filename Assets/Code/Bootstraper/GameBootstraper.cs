using Code.Data;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.Services.SceneLoader;
using UnityEngine;
using Zenject;

namespace Code.Bootstraper
{
    public class GameBootstraper : MonoBehaviour
    {
        private ISceneLoaderService _sceneLoaderService;
        private IPersistentProgressService _persistentProgressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService,
            IPersistentProgressService persistentProgressService, ISaveLoadService saveLoadService)
        {
            _sceneLoaderService = sceneLoaderService;
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;
        }

        private void Start()
        {
            LoadProgressOrInitNew();
            _sceneLoaderService.Load(sceneName: Scenes.MainMenu.ToString(), delay: 1.5f);
        }

        private void LoadProgressOrInitNew()
        {
            _persistentProgressService.UserProgress =
                _saveLoadService.LoadProgress() ?? new UserProgress();
        }
    }
}