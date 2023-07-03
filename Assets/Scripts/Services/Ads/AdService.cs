using System;
using System.Collections.Generic;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using Services.PersistentProgress;
using UnityEngine;

namespace Services.Ads
{
    public class AdService : IAdService, IAppodealInitializationListener, IRewardedVideoAdListener
    {
        private const string AppKey = "48b0f2391352a558132a69891b2399a44eb4440a1ec83ebd";

        public event Action RewardedVideoFinished;

        private readonly IPersistentProgressService _progressService;

        public AdService(IPersistentProgressService progressService)
        {
            _progressService = progressService;
            Appodeal.setRewardedVideoCallbacks(this);
            Initialization();
        }

        private void Initialization()
        {
            Appodeal.muteVideosIfCallsMuted(true);
            
            int adTypes = Appodeal.BANNER | Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO;
            Appodeal.initialize(AppKey, adTypes, this);
        }

        public void onInitializationFinished(List<string> errors)
        {
        }

        public void ShowAdBanner()
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                if (_progressService.UserProgress.AdsData.Activity)
                {
                    if (Appodeal.isLoaded(Appodeal.BANNER))
                        Appodeal.show(Appodeal.BANNER_BOTTOM);
                }
            }
        }

        public void HideAdBanner() =>
            Appodeal.hide(Appodeal.BANNER);

        public void ShowInterstitialAd()
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                if (_progressService.UserProgress.AdsData.Activity)
                {
                    if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
                        Appodeal.show(Appodeal.INTERSTITIAL);
                }
            }
        }

        public void ShowRewardedAd()
        {
            if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
                Appodeal.show(Appodeal.REWARDED_VIDEO);
        }

        public void onRewardedVideoFinished(double amount, string name)
        {
            _progressService.UserProgress.AddCoins(350);
            _progressService.UserProgress.AdsData.NumberOfBonuses -= 1;
            RewardedVideoFinished?.Invoke();
        }

        #region Appodeal (other methods)
        public void onRewardedVideoLoaded(bool precache)
        {
        }

        public void onRewardedVideoFailedToLoad()
        {
        }

        public void onRewardedVideoShowFailed()
        {
        }

        public void onRewardedVideoShown()
        {
        }

        public void onRewardedVideoClosed(bool finished)
        {
        }

        public void onRewardedVideoExpired()
        {
        }

        public void onRewardedVideoClicked()
        {
        }
        #endregion
    }
}