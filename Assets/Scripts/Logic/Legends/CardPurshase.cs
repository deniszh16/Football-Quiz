using Logic.Helpers;
using Services.Analytics;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.Legends
{
    public class CardPurshase : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Legends _legends;
        [SerializeField] private Card _card;
        
        [Header("Кнопка покупки")]
        [SerializeField] private Button _button;
        [SerializeField] private Image _imageButton;
        
        [Header("Открытая карточка")]
        [SerializeField] private Sprite _openCard;
        
        [Header("Эффект открытия")]
        [SerializeField] private Animator _effect;

        private const int Price = 950;

        private void Awake()
        {
            _button.onClick.AddListener(BuyCard);
            _card.CardPurchased += CheckAvailability;
        }

        private void CheckAvailability()
        {
            ReplaceSprite();
            _button.onClick.RemoveListener(BuyCard);
        }

        private void BuyCard()
        {
            if (_legends.ProgressService.GetUserProgress.CheckAmountCoins(Price))
            {
                _legends.ProgressService.GetUserProgress.SubtractionCoins(Price);
                _card.UpdateAvailability();
                _legends.ProgressService.GetUserProgress.LegendsData.Legends[_card.Number - ForArrays.MinusOne] =
                    LegendStatus.Opened;
                _legends.ProgressService.GetUserProgress.LegendsData.ReceivedCards += 1;
                _legends.SaveLoadService.SaveProgress();

                ShowEffect();
                ReplaceSprite();
                _button.onClick.RemoveListener(BuyCard);
                
                _legends.FirebaseService.SubmitAnEvent(id: AnalyticsKeys.BuyingCard);
            }
        }
        
        private void ReplaceSprite() =>
            _imageButton.sprite = _openCard;
        
        private void ShowEffect()
        {
            _effect.gameObject.SetActive(true);
            _effect.Rebind();
        }

        private void OnDestroy() =>
            _button.onClick.RemoveListener(BuyCard);
    }
}