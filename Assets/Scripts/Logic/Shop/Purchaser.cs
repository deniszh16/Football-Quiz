using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

namespace Logic.Shop
{
    public class Purchaser : MonoBehaviour
    {
        [SerializeField] private IAPListener _iapListener;

        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        private void Awake() =>
            _iapListener.onPurchaseComplete.AddListener(OnPurshaseCompleted);

        private void OnPurshaseCompleted(Product product)
        {
            switch (product.definition.id)
            {
                case Products.Ads:
                    RemoveAds();
                    break;
                case Products.CoinsX1:
                    BuyingCoins(1000);
                    break;
                case Products.CoinsX5:
                    BuyingCoins(5000);
                    break;
            }
        }

        private void RemoveAds()
        {
            _progressService.UserProgress.AdsData.ChangeAvailability();
            _saveLoadService.SaveProgress();
        }

        private void BuyingCoins(int value)
        {
            _progressService.UserProgress.AddCoins(value);
            _saveLoadService.SaveProgress();
        }

        private void OnDestroy() =>
            _iapListener.onPurchaseComplete.RemoveListener(OnPurshaseCompleted);
    }
}