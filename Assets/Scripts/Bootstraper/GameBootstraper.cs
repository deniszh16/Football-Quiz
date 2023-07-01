using Data;
using Services.MigrationOldProgress;
using Services.PersistentProgress;
using Services.SaveLoad;
using Services.SceneLoader;
using UnityEngine;
using Zenject;

namespace Bootstraper
{
    public class GameBootstraper : MonoBehaviour
    {
        private ISceneLoaderService _sceneLoaderService;
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private IMigrationOldProgressService _migrationService;

        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService, IPersistentProgressService progressService,
            ISaveLoadService saveLoadService, IMigrationOldProgressService migrationService)
        {
            _sceneLoaderService = sceneLoaderService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _migrationService = migrationService;
        }

        private void Start()
        {
            LoadProgressOrInitNew();
            _migrationService.CheckOldProgress();
            _sceneLoaderService.Load(sceneName: Scenes.MainMenu.ToString(), delay: 1.5f);
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.UserProgress =
                _saveLoadService.LoadProgress() ?? new UserProgress();
        }
    }
}