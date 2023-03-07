using Code.Logic.Helpers;
using Code.Services.SceneLoader;
using Code.StaticData.Questions.Facts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Logic.Facts
{
    public class CategoryOpening : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Category _category;
        
        [Header("Кнопка категории")]
        [SerializeField] private Button _button;

        private ISceneLoaderService _sceneLoaderService;

        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService) =>
            _sceneLoaderService = sceneLoaderService;

        private void Start() =>
            _button.onClick.AddListener(OpenCategory);

        private void OpenCategory()
        {
            ActivePartition.SectionsGame = SectionsGame.Facts;
            ActivePartition.CategoryNumber = _category.Number;

            if (CheckCategoryCompletion() && CheckCategoryAvailability())
                _sceneLoaderService.Load(Scenes.FactsQuestions.ToString(), 0f);
            else
                _sceneLoaderService.Load(Scenes.Results.ToString(), 0f);
        }

        private bool CheckCategoryCompletion() =>
            _category.CurrentQuestion < _category.TotalQuestions;

        private bool CheckCategoryAvailability() =>
            _category.Availability == FactsAccessibility.Available;

        private void OnDestroy() =>
            _button.onClick.AddListener(OpenCategory);
    }
}