using UnityEngine;
//using AppodealAds.Unity.Api;

namespace Cubra
{
    public class Ads : MonoBehaviour
    {
        protected virtual void Start()
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                // Если реклама загружена
                //if (Appodeal.isLoaded(Appodeal.BANNER))
                    //Appodeal.show(Appodeal.BANNER_BOTTOM);
            }
        }
    }
}