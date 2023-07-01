using Logic.Helpers;
using Services.SceneLoader;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.Countries
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

            _sceneLoader.Load(
                _currentСategory.CurrentQuestion > 0 && CheckCategoryCompletion()
                    ? Scenes.CountriesQuestions.ToString()
                    : Scenes.Results.ToString());
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