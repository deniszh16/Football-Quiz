using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;

namespace DZGames.Football.Services
{
    public class AdService : IAdService
    {
        private const string AppKey = "48b0f2391352a558132a69891b2399a44eb4440a1ec83ebd";
        
        public void Initialization()
        {
            Appodeal.MuteVideosIfCallsMuted(true);
            int adTypes = AppodealAdType.Interstitial | AppodealAdType.Banner | AppodealAdType.RewardedVideo;
            AppodealCallbacks.Sdk.OnInitialized += OnInitializationFinished;
            Appodeal.Initialize(AppKey, adTypes);
        }
        
        private void OnInitializationFinished(object sender, SdkInitializedEventArgs e) {}
        
        public void ShowAdBanner() =>
            Appodeal.Show(AppodealShowStyle.BannerBottom);
        
        public void HideAdBanner() =>
            Appodeal.Hide(AppodealAdType.Banner);

        public void DestroyAdBanner() =>
            Appodeal.Destroy(AppodealAdType.Banner);

        public void ShowInterstitialAd()
        {
            if (Appodeal.IsLoaded(AppodealAdType.Interstitial))
                Appodeal.Show(AppodealShowStyle.Interstitial);
        }
        
        public void ShowRewardedAd()
        {
            if (Appodeal.IsLoaded(AppodealAdType.RewardedVideo))
                Appodeal.Show(AppodealShowStyle.RewardedVideo);
        }
    }
}