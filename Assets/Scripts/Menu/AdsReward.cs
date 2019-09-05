using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class AdsReward : Ads, IRewardedVideoAdListener
{
    [Header("Компонент бонуса")]
    [SerializeField] private Bonus bonus;

    private Statistics statistics;

    private void Awake()
    {
        bonus = bonus.GetComponent<Bonus>();
        statistics = Camera.main.GetComponent<Statistics>();
    }

    protected override void Start()
    {
        // Отображение баннера
        base.Start();

        // Активация обратных вызовов для видеорекламы
        Appodeal.setRewardedVideoCallbacks(this);
    }

    // Просмотр видеорекламы с вознаграждением
    public void ShowRewardedVideo()
    {
        // Если реклама загружена
        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
            // Отображаем рекламный ролик
            Appodeal.show(Appodeal.REWARDED_VIDEO);
    }

    public void onRewardedVideoClicked() {}
    public void onRewardedVideoClosed(bool finished) {}
    public void onRewardedVideoExpired() {}
    public void onRewardedVideoFailedToLoad() {}
    // Успешный просмотр видеорекламы с вознаграждением
    public void onRewardedVideoFinished(double amount, string name)
    {
        // Уменьшаем количество бонусных просмотров
        bonus.DecreaseViews();
        // Добавляем бонусные монеты и обновляем статистику
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 250);
        statistics.UpdateCoins();
    }
    public void onRewardedVideoLoaded(bool precache) {}
    public void onRewardedVideoShown() {}
}