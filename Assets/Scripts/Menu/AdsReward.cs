using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class AdsReward : AdsBanner, IRewardedVideoAdListener
{
    [Header("Компонент бонуса")]
    [SerializeField] private Bonus bonus;

    // Ссылка на статистику
    private Statistics statistics;

    private void Awake()
    {
        bonus = bonus.GetComponent<Bonus>();
        statistics = Camera.main.GetComponent<Statistics>();
    }

    protected override void Start()
    {
        // Показываем баннер
        base.Start();

        // Активируем обратные вызовы для видеорекламы
        Appodeal.setRewardedVideoCallbacks(this);
    }

    /// <summary>Просмотр видеорекламы с вознаграждением</summary>
    public void ShowRewardedVideo()
    {
        // Если реклама загружена
        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
            // Отображаем рекламный ролик
            Appodeal.show(Appodeal.REWARDED_VIDEO);
    }

    #region Appodeal (on rewarded video)
    public void onRewardedVideoClicked() {}
    public void onRewardedVideoClosed(bool finished) {}
    public void onRewardedVideoExpired() {}
    public void onRewardedVideoFailedToLoad() {}

    /// <summary>Успешный просмотр видеорекламы с вознаграждением</summary>
    public void onRewardedVideoFinished(double amount, string name)
    {
        // Уменьшаем количество просмотров
        bonus.DecreaseViews();

        // Добавляем бонусные монеты
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 250);
        // Обновляем статистику
        statistics.UpdateCoins();
    }

    public void onRewardedVideoLoaded(bool precache) {}
    public void onRewardedVideoShown() {}
    #endregion
}