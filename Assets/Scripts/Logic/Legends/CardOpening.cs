using DZGames.Football.Helpers;
using DZGames.Football.Services;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace DZGames.Football.Legends
{
    public class CardOpening : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Card _card;
        
        [Header("Кнопка открытия")]
        [SerializeField] private Button _button;

        private ISceneLoaderService _sceneLoaderService;

        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService) =>
            _sceneLoaderService = sceneLoaderService;

        private void Awake() =>
            _card.CardPurchased += CheckAvailability;

        private void OnDestroy()
        {
            _card.CardPurchased -= CheckAvailability;
            _button.onClick.RemoveListener(OpenCard);
        }

        private void CheckAvailability()
        {
            if (_card.IsAvailable && _card.Biography)
                _button.onClick.AddListener(OpenCard);
        }

        private void OpenCard()
        {
            ActivePartition.CategoryNumber = _card.Number;
            _sceneLoaderService.Load(Scenes.Biography.ToString());
        }
    }
}