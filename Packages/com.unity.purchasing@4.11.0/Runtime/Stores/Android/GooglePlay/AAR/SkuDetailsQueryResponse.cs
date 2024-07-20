using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Purchasing.Interfaces;
using UnityEngine.Purchasing.Models;
namespace UnityEngine.Purchasing
{
    class SkuDetailsQueryResponse : ISkuDetailsQueryResponse
    {
        readonly ConcurrentBag<(IGoogleBillingResult, IEnumerable<AndroidJavaObject>)> m_Responses = new ConcurrentBag<(IGoogleBillingResult, IEnumerable<AndroidJavaObject>)>();

        ~SkuDetailsQueryResponse()
        {
            foreach (var response in m_Responses)
            {
                var objList = response.Item2;
                if (objList == null)
                {
                    continue;
                }

                foreach (var obj in objList)
                {
                    obj?.Dispose();
                }
            }
        }

        public void AddResponse(IGoogleBillingResult billingResult, IEnumerable<AndroidJavaObject> skuDetails)
        {
#if UNITY_2021_1_OR_NEWER
            m_Responses.Add((billingResult, skuDetails.Select(sku => sku.CloneReference()).ToList()));
#else
            m_Responses.Add((billingResult, skuDetails.Select(sku => sku).ToList()));
#endif
        }

        public List<AndroidJavaObject> SkuDetails()
        {
            return m_Responses.Where(response => response.Item1.responseCode == GoogleBillingResponseCode.Ok)
                .SelectMany(response => response.Item2).ToList();
        }

        public bool IsRecoverable()
        {
            return m_Responses.Select(response => response.Item1).Any(IsRecoverable);
        }

        static bool IsRecoverable(IGoogleBillingResult billingResult)
        {
            return billingResult.responseCode == GoogleBillingResponseCode.ServiceUnavailable || billingResult.responseCode == GoogleBillingResponseCode.DeveloperError;
        }
    }
}
