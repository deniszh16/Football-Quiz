using System;

namespace Services.Ads
{
    public interface IAdService
    {
        public event Action RewardedVideoFinished;
        public void ShowAdBanner();
        public void HideAdBanner();
        public void ShowRewardedAd();
        public void ShowInterstitialAd();
    }
}