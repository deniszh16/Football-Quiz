using Logic.Helpers;
using Services.Analytics;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.Countries
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

        private const string AnalyticsKey = "countries_open_category";
        
        private ISaveLoadService _saveLoadService;
        private IFirebaseService _firebaseService;

        [Inject]
        private void Construct(ISaveLoadService saveLoadService, IFirebaseService firebaseService)
        {
            _saveLoadService = saveLoadService;
            _firebaseService = firebaseService;
        }

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
            if (_currentСategory.ProgressService.GetUserProgress.CheckAmountCoins(_price))
            {
                _currentСategory.ProgressService.GetUserProgress.SubtractionCoins(_price);

                _currentСategory.CurrentQuestion = 1;
                _currentСategory.IsAvailable = CategoryAccessibility.Available;
                _currentСategory.ProgressService.GetUserProgress.CountriesData.Sets[_currentСategory.Number - ForArrays.MinusOne]= _currentСategory.CurrentQuestion;
                _currentСategory.ProgressService.GetUserProgress.CountriesData.Accessibility[
                    _currentСategory.Number - ForArrays.MinusOne] = CategoryAccessibility.Available; 
                _saveLoadService.SaveProgress();
                
                ShowEffect();
                ReplaceSprite();

                _button.onClick.RemoveListener(BuyCategory);
                _currentСategory.ReportCategoryPurchase();
                
                _firebaseService.SubmitAnEvent(id: AnalyticsKey);
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