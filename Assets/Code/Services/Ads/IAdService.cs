using System;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;

namespace Code.Services.Ads
{
    public interface IAdService
    {
        public event Action RewardedVideoFinished;
        public void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService);
        public void ShowAdBanner();
        public void HideAdBanner();
        public void ShowRewardedAd();
        public void ShowInterstitialAd();
    }
}