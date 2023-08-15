namespace Services.Ads
{
    public interface IAdService
    {
        public void ShowAdBanner();
        public void HideAdBanner();
        public void ShowRewardedAd();
        public void ShowInterstitialAd();
    }
}