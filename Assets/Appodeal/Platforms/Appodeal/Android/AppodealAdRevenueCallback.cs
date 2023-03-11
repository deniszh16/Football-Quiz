using System.Diagnostics.CodeAnalysis;
using AppodealAds.Unity.Common;

// ReSharper Disable CheckNamespace
namespace AppodealAds.Unity.Android
{
    /// <summary>
    /// Android implementation of <see langword="IAdRevenueListener"/> interface.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class AppodealAdRevenueCallback
#if UNITY_ANDROID
        : UnityEngine.AndroidJavaProxy
    {
        private readonly IAdRevenueListener _listener;
        
        internal AppodealAdRevenueCallback(IAdRevenueListener listener) : base("com.appodeal.ads.revenue.AdRevenueCallbacks")
        {
            _listener = listener;
        }
        
        private void onAdRevenueReceive(UnityEngine.AndroidJavaObject ad)
        {
            _listener?.onAdRevenueReceived(
                new AppodealAdRevenue
                {
                    AdType = ad.Call<string>("getAdTypeString"),
                    NetworkName = ad.Call<string>("getNetworkName"),
                    AdUnitName = ad.Call<string>("getAdUnitName"),
                    DemandSource = ad.Call<string>("getDemandSource"),
                    Placement = ad.Call<string>("getPlacement"),
                    Revenue = ad.Call<double>("getRevenue"),
                    Currency = ad.Call<string>("getCurrency"),
                    RevenuePrecision = ad.Call<string>("getRevenuePrecision")
                }
            );
        }
    }
#else
    {
        public AppodealAdRevenueCallback(IAdRevenueListener listener) { }
    }
#endif
}
