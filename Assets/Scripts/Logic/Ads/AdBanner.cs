using Services.PersistentProgress;
using Services.Ads;
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
            _progressService.GetUserProgress.AdsData.AvailabilityChanged += _adService.HideAdBanner;

        private void Start()
        {
            if (_progressService.GetUserProgress.AdsData.Activity)
                _adService.ShowAdBanner();
        }

        private void OnDestroy() =>
            _progressService.GetUserProgress.AdsData.AvailabilityChanged -= _adService.HideAdBanner;
    }
}