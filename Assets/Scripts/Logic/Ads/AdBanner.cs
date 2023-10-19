using Services.Ads;
using Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace Logic.Ads
{
    public class AdBanner : MonoBehaviour
    {
        private IAdService _adService;
        private IPersistentProgressService _progressService;

        [Inject]
        private void Construct(IAdService adService, IPersistentProgressService progressService)
        {
            _adService = adService;
            _progressService = progressService;
        }

        private void Awake() =>
            _progressService.UserProgress.AdsData.AvailabilityChanged += _adService.HideAdBanner;

        private void Start()
        {
            if (_progressService.UserProgress.AdsData.Activity)
                _adService.ShowAdBanner();
        }

        private void OnDestroy() =>
            _progressService.UserProgress.AdsData.AvailabilityChanged -= _adService.HideAdBanner;
    }
}