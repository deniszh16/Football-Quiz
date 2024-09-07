using DZGames.Football.StaticData.Facts;
using DZGames.Football.Services;
using DZGames.Football.Helpers;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace DZGames.Football.Facts
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
        
        private void OnDestroy() =>
            _button.onClick.RemoveListener(OpenCategory);

        private void OpenCategory()
        {
            ActivePartition.SectionsGame = SectionsGame.Facts;
            ActivePartition.CategoryNumber = _category.Number;

            if (CheckCategoryCompletion() && CheckCategoryAvailability())
            {
                _sceneLoaderService.Load(Scenes.FactsQuestions.ToString());
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
    }
}