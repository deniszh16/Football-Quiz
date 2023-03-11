// ReSharper Disable CheckNamespace
namespace AppodealAds.Unity.Common
{
    /// <summary>
    /// Interface containing signature of Appodeal Ad Revenue callback method.
    /// </summary>
    public interface IAdRevenueListener
    {
        /// <summary>
        /// <para>
        /// Fires when Appodeal SDK tracks ad impression.
        /// </para>
        /// See <see href=""/> for more details.
        /// </summary>
        /// <param name="ad">contains info about the tracked impression.</param>
        void onAdRevenueReceived(AppodealAdRevenue ad);
    }
}
