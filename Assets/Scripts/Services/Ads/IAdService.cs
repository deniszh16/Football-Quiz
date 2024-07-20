namespace DZGames.Football.Services
{
    public interface IAdService
    {
        public void Initialization();
        public void ShowAdBanner();
        public void HideAdBanner();
        public void DestroyAdBanner();
        public void ShowRewardedAd();
        public void ShowInterstitialAd();
    }
}