using Code.Services.Ads;
using Code.Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace Code.Logic.Ads
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

        private void Start() =>
            _adService.ShowAdBanner();

        private void OnDestroy() =>
            _progressService.UserProgress.AdsData.AvailabilityChanged -= _adService.HideAdBanner;
    }
}