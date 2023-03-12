using Code.Logic.Helpers;
using Code.Services.SceneLoader;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Logic.Countries
{
    public class CategoryOpening : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Category _currentСategory;
        
        [Header("Кнопка категории")]
        [SerializeField] private Button _button;
        
        private ISceneLoaderService _sceneLoader;

        [Inject]
        private void Construct(ISceneLoaderService sceneLoader) =>
            _sceneLoader = sceneLoader;

        private void Awake() =>
            _currentСategory.CategoryPurchased += CheckAvailability;

        private void Start() =>
            CheckAvailability();

        private void CheckAvailability()
        {
            if (_currentСategory.IsAvailable == CategoryAccessibility.Available)
                _button.onClick.AddListener(OpenCategory);
        }

        private void OpenCategory()
        {
            ActivePartition.SectionsGame = SectionsGame.Countries;
            ActivePartition.CategoryNumber = _currentСategory.Number;

            if (_currentСategory.CurrentQuestion > 0 && CheckCategoryCompletion())
                _sceneLoader.Load(Scenes.CountriesQuestions.ToString(), 0f);
            else
                _sceneLoader.Load(Scenes.Results.ToString(), 0f);
        }

        private bool CheckCategoryCompletion() =>
            _currentСategory.CurrentQuestion <= _currentСategory.StaticDataService
                .GetCountriesCategory(_currentСategory.Number).Questions.Count;

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OpenCategory);
            _currentСategory.CategoryPurchased -= CheckAvailability;
        }
    }
}