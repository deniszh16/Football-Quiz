using DZGames.Football.Services;
using UnityEngine;
using VContainer;

namespace DZGames.Football.Ads
{
    public class AdBanner : MonoBehaviour
    {
        [Header("Отключение рекламы на сцене")]
        [SerializeField] private bool _disablingAds;
        
        private IAdService _adService;
        private IPersistentProgressService _progressService;

        [Inject]
        private void Construct(IAdService adService, IPersistentProgressService progressService)
        {
            _adService = adService;
            _progressService = progressService;
        }

        private void Awake()
        {
            _progressService.GetUserProgress.AdsData.AvailabilityChanged += _adService.HideAdBanner;
            _progressService.GetUserProgress.AdsData.AvailabilityChanged += _adService.DestroyAdBanner;
        }

        private void Start()
        {
            if (_progressService.GetUserProgress.AdsData.Activity && _disablingAds == false)
            {
                _adService.ShowAdBanner();
                return;
            }
            
            if (_disablingAds)
                _adService.HideAdBanner();
        }

        private void OnDestroy()
        {
            _progressService.GetUserProgress.AdsData.AvailabilityChanged -= _adService.HideAdBanner;
            _progressService.GetUserProgress.AdsData.AvailabilityChanged -= _adService.DestroyAdBanner;
        }
    }
}