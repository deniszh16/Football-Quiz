﻿using DZGames.Football.Services;
using UnityEngine;
using UnityEngine.Purchasing;
using VContainer;

namespace DZGames.Football.Shop
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
            _iapListener.onPurchaseComplete.AddListener(OnPurchaseCompleted);

        private void OnDestroy() =>
            _iapListener.onPurchaseComplete.RemoveListener(OnPurchaseCompleted);

        private void OnPurchaseCompleted(Product product)
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
            _progressService.GetUserProgress.AdsData.ChangeAvailability();
            _saveLoadService.SaveProgress();
        }

        private void BuyingCoins(int value)
        {
            _progressService.GetUserProgress.AddCoins(value);
            _saveLoadService.SaveProgress();
        }
    }
}