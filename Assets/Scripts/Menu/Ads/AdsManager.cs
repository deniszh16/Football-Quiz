using System;
using UnityEngine;
using AppodealAds.Unity.Api;

namespace Cubra
{
    public class AdsManager : MonoBehaviour
    {
        [Header("Идентификатор рекламы")]
        [SerializeField] private string _key;

        private void Awake()
        {
            if (PlayerPrefs.GetInt("date") != DateTime.Now.Day)
            {
                // Открываем ежедневный бонус
                PlayerPrefs.SetInt("bonus", 3);
                PlayerPrefs.SetInt("date", DateTime.Now.Day);
            }
        }

        private void Start()
        {
            // Отключаем рекламные звуки
            Appodeal.muteVideosIfCallsMuted(true);

            // Инициализируем рекламный баннер, полноэкранный баннер и видеорекламу с вознаграждением
            Appodeal.initialize(_key, Appodeal.BANNER | Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO, true);
        }
    }
}