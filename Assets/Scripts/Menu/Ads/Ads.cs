using UnityEngine;
using AppodealAds.Unity.Api;

namespace Cubra
{
    public class Ads : MonoBehaviour
    {
        protected virtual void Start()
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                // Если реклама не отключена
                if (PlayerPrefs.GetString("show-ads") == "yes")
                {
                    if (Appodeal.isLoaded(Appodeal.BANNER))
                        Appodeal.show(Appodeal.BANNER_BOTTOM);
                }
            }
        }

        /// <summary>
        /// Скрытие рекламных баннеров
        /// </summary>
        public void HideAds()
        {
            Appodeal.hide(Appodeal.BANNER);
        }
    }
}