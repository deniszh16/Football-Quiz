using StaticData.Questions.Facts;
using Services.SceneLoader;
using Services.Analytics;
using Logic.Helpers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.Facts
{
    public class CategoryOpening : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Category _category;
        
        [Header("Кнопка категории")]
        [SerializeField] private Button _button;

        private ISceneLoaderService _sceneLoaderService;
        private IFirebaseService _firebaseService;

        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService, IFirebaseService firebaseService)
        {
            _sceneLoaderService = sceneLoaderService;
            _firebaseService = firebaseService;
        }

        private void Start() =>
            _button.onClick.AddListener(OpenCategory);

        private void OpenCategory()
        {
            ActivePartition.SectionsGame = SectionsGame.Facts;
            ActivePartition.CategoryNumber = _category.Number;

            if (CheckCategoryCompletion() && CheckCategoryAvailability())
            {
                _sceneLoaderService.Load(Scenes.FactsQuestions.ToString());
                _firebaseService.SubmitAnEvent(AnalyticsKeys.FactsOpenCategory, ("number", _category.Number));
            }
            else
            {
                _sceneLoaderService.Load(Scenes.Results.ToString());
            }
        }

        private bool CheckCategoryCompletion() =>
            _category.CurrentQuestion < _category.TotalQuestions;

        private bool CheckCategoryAvailability() =>
            _category.Availability == FactsAccessibility.Available;

        private void OnDestroy() =>
            _button.onClick.AddListener(OpenCategory);
    }
}