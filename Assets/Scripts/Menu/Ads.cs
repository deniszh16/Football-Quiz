using UnityEngine;
using AppodealAds.Unity.Api;

public class Ads : MonoBehaviour
{
    protected virtual void Start()
    {
        // Если доступен интернет, отображаем баннер
        if (Application.internetReachability != NetworkReachability.NotReachable) ShowBanner();
    }

    // Отображение рекламного баннера
    private void ShowBanner()
    {
        // Если реклама загружена
        if (Appodeal.isLoaded(Appodeal.BANNER))
            // Отображаем баннер внизу экрана
            Appodeal.show(Appodeal.BANNER_BOTTOM);
    }
}