using DZGames.Football.Data;
using DZGames.Football.Services;
using UnityEngine;
using VContainer;

namespace DZGames.Football.Bootstraper
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
            _sceneLoaderService.Load(sceneName: Scenes.MainMenu.ToString(), delay: 1f);
        }

        private void LoadProgressOrInitNew() =>
            _progressService.SetUserProgress(_saveLoadService.LoadProgress() ?? new UserProgress());
    }
}