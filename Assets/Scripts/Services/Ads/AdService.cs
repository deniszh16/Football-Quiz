using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace Services.Ads
{
    public class AdService : MonoBehaviour, IAdService
    {
        [Header("Ключ приложения")]
        [SerializeField] private string _appKey;

        private IPersistentProgressService _progressService;

        [Inject]
        private void Construct(IPersistentProgressService progressService) =>
            _progressService = progressService;

        private void Start()
        {
            Initialization();
        }

        private void Initialization()
        {
            int adTypes = AppodealAdType.Interstitial | AppodealAdType.Banner | AppodealAdType.RewardedVideo;
            AppodealCallbacks.Sdk.OnInitialized += OnInitializationFinished;
            Appodeal.Initialize(_appKey, adTypes);
        }
        
        private void OnInitializationFinished(object sender, SdkInitializedEventArgs e)
        {
        }

        public void ShowAdBanner()
        {
            if (_progressService.UserProgress.AdsData.Activity)
            {
                if (Appodeal.IsLoaded(AppodealAdType.Banner))
                    Appodeal.Show(AppodealShowStyle.BannerBottom);
            }
        }

        public void HideAdBanner() =>
            Appodeal.Hide(AppodealAdType.Banner);

        public void ShowInterstitialAd()
        {
            if (_progressService.UserProgress.AdsData.Activity)
            {
                if (Appodeal.IsLoaded(AppodealAdType.Interstitial))
                    Appodeal.Show(AppodealShowStyle.Interstitial);
            }
        }

        public void ShowRewardedAd()
        {
            if (Appodeal.IsLoaded(AppodealAdType.RewardedVideo))
                Appodeal.Show(AppodealShowStyle.RewardedVideo);
        }
    }
}