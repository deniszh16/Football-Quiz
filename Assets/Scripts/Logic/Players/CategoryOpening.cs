using Logic.Helpers;
using Services.PersistentProgress;
using Services.SceneLoader;
using Services.StaticData;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.Players
{
    public class CategoryOpening : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Category _currentСategory;
        
        [Header("Кнопка открытия")]
        [SerializeField] private Button _button;

        private int _numberOfTasks;
        
        private IPersistentProgressService _progressService;
        private ISceneLoaderService _sceneLoaderService;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISceneLoaderService sceneLoaderService,
            IStaticDataService staticDataService)
        {
            _progressService = progressService;
            _sceneLoaderService = sceneLoaderService;
            _staticDataService = staticDataService;
        }

        private void Awake() =>
            _button.onClick.AddListener(OpenCategory);

        private void Start() =>
            _numberOfTasks = _staticDataService.GetPlayersCategory(_currentСategory.Number).Questions.Count;

        private void OpenCategory()
        {
            ActivePartition.SectionsGame = SectionsGame.Players;
            ActivePartition.CategoryNumber = _currentСategory.Number;

            _sceneLoaderService.Load(
                _progressService.GetUserProgress.PlayersData.Sets[_currentСategory.Number - 1] < _numberOfTasks
                    ? Scenes.PlayersQuestions.ToString()
                    : Scenes.Results.ToString());
        }

        private void OnDestroy() =>
            _button.onClick.RemoveListener(OpenCategory);
    }
}