﻿using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using AppodealAds.Unity.Api;
using ConsentManager;

namespace AppodealAds.Unity.Common
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public interface IAppodealAdsClient
    {
        void initialize(string appKey, int adTypes, IAppodealInitializationListener listener);
        bool isInitialized(int adType);
        bool show(int adTypes);
        bool show(int adTypes, string placement);
        bool isLoaded(int adTypes);
        void cache(int adTypes);
        void hide(int adTypes);
        void setAutoCache(int adTypes, bool autoCache);
        bool isPrecache(int adTypes);
        bool showBannerView(int YAxis, int XGravity, string Placement);
        bool showMrecView(int YAxis, int XGravity, string Placement);
        void hideBannerView();
        void hideMrecView();
        void setSmartBanners(bool value);
        bool isSmartBannersEnabled();
        void setBannerAnimation(bool value);
        void setTabletBanners(bool value);
        void setBannerRotation(int leftBannerRotation, int rightBannerRotation);
        void setTesting(bool test);
        void setLogLevel(Appodeal.LogLevel level);
        void setChildDirectedTreatment(bool value);
        void updateConsent(Consent consent);
        void updateGdprConsent(Appodeal.GdprUserConsent consent);
        void updateCcpaConsent(Appodeal.CcpaUserConsent consent);
        List<string> getNetworks(int adType);
        void disableNetwork(string network);
        void disableNetwork(string network, int type);
        void disableLocationPermissionCheck();
        void muteVideosIfCallsMuted(bool value);
        void showTestScreen();
        string getVersion();
        bool canShow(int adTypes);
        bool canShow(int adTypes, string placement);
        void setCustomFilter(string name, bool value);
        void setCustomFilter(string name, int value);
        void setCustomFilter(string name, double value);
        void setCustomFilter(string name, string value);
        void resetCustomFilter(string name);
        void setExtraData(string key, bool value);
        void setExtraData(string key, int value);
        void setExtraData(string key, double value);
        void setExtraData(string key, string value);
        void resetExtraData(string key);
        string getRewardCurrency(string placement);
        double getRewardAmount(string placement);
        string getRewardCurrency();
        double getRewardAmount();
        double getPredictedEcpm(int adTypes);
        void setTriggerOnLoadedOnPrecache(int adTypes, bool onLoadedTriggerBoth);
        void setUserId(string id);
        string getUserId();
        long getSegmentId();
        void trackInAppPurchase(double amount, string currency);
        void setInterstitialCallbacks(IInterstitialAdListener listener);
        void setRewardedVideoCallbacks(IRewardedVideoAdListener listener);
        void setBannerCallbacks(IBannerAdListener listener);
        void setMrecCallbacks(IMrecAdListener listener);
        void setAdRevenueCallback(IAdRevenueListener listener);
        void destroy(int adTypes);
        void setUseSafeArea(bool value);
        bool isAutoCacheEnabled(int adType);
        void logEvent(string eventName, Dictionary<string, object> eventParams);
        void validatePlayStoreInAppPurchase(IPlayStoreInAppPurchase purchase, IInAppPurchaseValidationListener listener);
        void validateAppStoreInAppPurchase(IAppStoreInAppPurchase purchase, IInAppPurchaseValidationListener listener);

        #region Deprecated signatures

        void initialize(string appKey, int type);
        void initialize(string appKey, int type, bool hasConsent);
        void initialize(string appKey, int adTypes, Consent consent);
        void updateConsent(bool value);
        void setSharedAdsInstanceAcrossActivities(bool value);
        void setUserAge(int age);
        void setUserGender(UserSettings.Gender gender);

        #endregion

    }
}
