using Code.Data;
using Code.Services.MigrationOldProgress;
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