using UnityEngine;
using AppodealAds.Unity.Api;

public class AdsManager : MonoBehaviour
{
    [Header("Идентификатор рекламы")]
    [SerializeField] private string key;

    private void Start()
    {
        // Отключаем звук рекламы, если отключены звонки
        Appodeal.muteVideosIfCallsMuted(true);
        // Инициализируем рекламный баннер и видео с вознаграждением
        Appodeal.initialize(key, Appodeal.BANNER | Appodeal.REWARDED_VIDEO, true);
    }
}