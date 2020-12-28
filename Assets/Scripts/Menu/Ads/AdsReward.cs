using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

namespace Cubra
{
    public class AdsReward : Ads, IRewardedVideoAdListener
    {
        [Header("Компонент бонуса")]
        [SerializeField] private DailyBonus _dailyBonus;

        [Header("Компонент заработанных очков")]
        [SerializeField] private PointsEarned _pointsEarned;

        protected override void Start()
        {
            base.Start();

            // Активируем обратные вызовы для видеорекламы
            Appodeal.setRewardedVideoCallbacks(this);
        }

        /// <summary>
        /// Просмотр видеорекламы с вознаграждением
        /// </summary>
        public void ShowRewardedVideo()
        {
            if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
                Appodeal.show(Appodeal.REWARDED_VIDEO);
        }

        /// <summary>
        /// Успешный просмотр видеорекламы
        /// </summary>
        public void onRewardedVideoFinished(double amount, string name)
        {
            _dailyBonus.UseBonus();

            // Добавляем бонусные монеты
            _pointsEarned.ChangeQuantityCoins(350);
        }

        #region Appodeal (other methods)
        public void onRewardedVideoClicked() {}
        public void onRewardedVideoClosed(bool finished) {}
        public void onRewardedVideoExpired() {}
        public void onRewardedVideoFailedToLoad() {}
        public void onRewardedVideoLoaded(bool precache) {}
        public void onRewardedVideoShown() {}
        public void onRewardedVideoShowFailed() {}
        #endregion
    }
}