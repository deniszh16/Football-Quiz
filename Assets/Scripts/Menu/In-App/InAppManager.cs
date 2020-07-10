using System;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Cubra
{
    public class InAppManager : MonoBehaviour, IStoreListener
    {
        private IStoreController _controller;
        private IExtensionProvider _extensions;

        // Идентификаторы товаров
        private readonly string _ads = "disable_ads";
        private readonly string _coinsX1 = "coins_1000";
        private readonly string _coinsX5 = "coins_5000";

        private PointsEarned _pointsEarned;
        private Ads _banners;

        private void Awake()
        {
            _pointsEarned = Camera.main.GetComponent<PointsEarned>();
            _banners = Camera.main.GetComponent<Ads>();
        }

        private void Start()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            
            builder.AddProduct(_ads, ProductType.NonConsumable);
            builder.AddProduct(_coinsX1, ProductType.Consumable);
            builder.AddProduct(_coinsX5, ProductType.Consumable);
            
            UnityPurchasing.Initialize(this, builder);
        }

        /// <summary>
        /// Инициализация Unity IAP
        /// </summary>
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _controller = controller;
            _extensions = extensions;
        }

        /// <summary>
        /// Выполнена ли инициализация Unity IAP
        /// </summary>
        private bool IsInitialized()
        {
            return _controller != null && _extensions != null;
        }

        /// <summary>
        /// Покупка отключения рекламы
        /// </summary>
        public void BuyDisableAds()
        {
            BuyProductID(_ads);
        }

        /// <summary>
        /// Покупка тысячи монет
        /// </summary>
        public void BuyThousandCoins()
        {
            BuyProductID(_coinsX1);
        }

        /// <summary>
        /// Покупка пяти тысяч монет
        /// </summary>
        public void BuyFiveThousandCoins()
        {
            BuyProductID(_coinsX5);
        }

        /// <summary>
        /// Покупка товара с указанным идентификатором
        /// </summary>
        private void BuyProductID(string productId)
        {
            if (IsInitialized())
            {
                Product product = _controller.products.WithID(productId);

                if (product != null && product.availableToPurchase)
                    _controller.InitiatePurchase(product);
                else
                    OnPurchaseFailed(product, PurchaseFailureReason.ProductUnavailable);
            }
        }

        /// <summary>
        /// Ошибка инициализации Unity IAP
        /// </summary>
        public void OnInitializeFailed(InitializationFailureReason error) {}

        /// <summary>
        /// Успешное завершение покупки
        /// </summary>
        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
        {
            if (string.Equals(e.purchasedProduct.definition.id, _ads, StringComparison.Ordinal))
            {
                PlayerPrefs.SetString("show-ads", "no");
                _banners.HideAds();
            }
            else if (string.Equals(e.purchasedProduct.definition.id, _coinsX1, StringComparison.Ordinal))
            {
                _pointsEarned.ChangeQuantityCoins(1000);
            }
            else if (string.Equals(e.purchasedProduct.definition.id, _coinsX5, StringComparison.Ordinal))
            {
                _pointsEarned.ChangeQuantityCoins(5000);
            }

            return PurchaseProcessingResult.Complete;
        }

        /// <summary>
        /// Неудачная покупка товара
        /// </summary>
        public void OnPurchaseFailed(Product i, PurchaseFailureReason p) {}

        /// <summary>
        /// Восстановление покупки (отключения рекламы)
        /// </summary>
        public void RestorePurchase()
        {
            if (_controller.products.WithID(_ads).hasReceipt)
            {
                PlayerPrefs.SetString("show-ads", "no");
                _banners.HideAds();
            }
        }
    }
}