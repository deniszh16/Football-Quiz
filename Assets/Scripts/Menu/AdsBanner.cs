using UnityEngine;
using AppodealAds.Unity.Api;

public class AdsBanner : MonoBehaviour
{
    protected virtual void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable) ShowBanner();
    }

    /// <summary>Отображение рекламного баннера</summary>
    private void ShowBanner()
    {
        // Если реклама загружена
        if (Appodeal.isLoaded(Appodeal.BANNER))
            // Отображаем баннер внизу экрана
            Appodeal.show(Appodeal.BANNER_BOTTOM);
    }
}