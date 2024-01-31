using Services.PersistentProgress;
using Services.SceneLoader;
using Services.SaveLoad;
using UnityEngine;
using Zenject;
using Data;

namespace Bootstraper
{
    public class GameBootstraper : MonoBehaviour
    {
        private ISceneLoaderService _sceneLoaderService;
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;


        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService, IPersistentProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _sceneLoaderService = sceneLoaderService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }
        
        private void Awake()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        private void Start()
        {
            LoadProgressOrInitNew();
            _sceneLoaderService.Load(sceneName: Scenes.MainMenu.ToString(), delay: 1.5f);
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.GetUserProgress =
                _saveLoadService.LoadProgress() ?? new UserProgress();
        }
    }
}