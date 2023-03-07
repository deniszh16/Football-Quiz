using Code.Logic.Helpers;
using Code.Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Logic.Countries
{
    public class CategoryPurshase : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Category _currentСategory;

        [Header("Кнопка категории")]
        [SerializeField] private Button _button;
        [SerializeField] private Image _imageButton;
        
        [Header("Стоимость открытия")]
        [SerializeField] private int _price;
        
        [Header("Открытая кнопка")]
        [SerializeField] private Sprite _sprite;
        
        [Header("Эффект открытия")]
        [SerializeField] private Animator _effect;
        
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(ISaveLoadService saveLoadService) =>
            _saveLoadService = saveLoadService;

        private void Start()
        {
            if (_currentСategory.IsAvailable == CategoryAccessibility.Available)
                ReplaceSprite();
            else
                _button.onClick.AddListener(BuyCategory);
        }

        private void ReplaceSprite() =>
            _imageButton.sprite = _sprite;

        private void BuyCategory()
        {
            if (_currentСategory.PersistentProgress.UserProgress.CheckAmountCoins(_price))
            {
                _currentСategory.PersistentProgress.UserProgress.SubtractionCoins(_price);

                _currentСategory.CurrentQuestion = 1;
                _currentСategory.IsAvailable = CategoryAccessibility.Available;
                _currentСategory.PersistentProgress.UserProgress.CountriesData.Sets[_currentСategory.Number - ForArrays.MinusOne]= _currentСategory.CurrentQuestion;
                _currentСategory.PersistentProgress.UserProgress.CountriesData.Accessibility[
                    _currentСategory.Number - ForArrays.MinusOne] = CategoryAccessibility.Available; 
                _saveLoadService.SaveProgress();
                
                ShowEffect();
                ReplaceSprite();

                _button.onClick.RemoveListener(BuyCategory);
                _currentСategory.ReportCategoryPurchase();
            }
        }

        private void ShowEffect()
        {
            _effect.gameObject.SetActive(true);
            _effect.Rebind();
        }

        private void OnDestroy() =>
            _button.onClick.RemoveListener(BuyCategory);
    }
}