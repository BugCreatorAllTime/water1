#if UNITY_PURCHASING && !UNITY_STANDALONE
#define ENABLE
#endif

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if ENABLE
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
#endif

// Placing the Purchaser class in the CompleteProject namespace allows it to interact with ScoreManager, 
// one of the existing Survival Shooter scripts.
namespace FMod
{
    // Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
    public class Purchaser : MonoBehaviour
#if ENABLE
        , IStoreListener
#endif
    {
        public static Purchaser Instance { get; private set; }

#if ENABLE
        private IStoreController m_StoreController;          // The Unity Purchasing system.
        private IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.  
#endif

        public event System.Action OnInitializedEvents;
#if ENABLE
        public event PurchaseCompletedHandler OnPurchaseCompleted;
#endif
        public event System.Action OnPurchaseFailedEvents;
        public event System.Action OnRestoreFailedEvent;

#if ENABLE
        public delegate void PurchaseCompletedHandler(string productId, Product product);
#endif
        // Product identifiers for all products capable of being purchased: 
        // "convenience" general identifiers for use with Purchasing, and their store-specific identifier 
        // counterparts for use with and outside of Unity Purchasing. Define store-specific identifiers 
        // also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

        // General product identifiers for the consumable, non-consumable, and subscription products.
        // Use these handles in the code to reference which product to purchase. Also use these values 
        // when defining the Product Identifiers on the store. Except, for illustration purposes, the 
        // kProductIDSubscription - it has custom Apple and Google identifiers. We declare their store-
        // specific mapping to Unity Purchasing's AddProduct, below.
        public List<string> consumableProductsIds = new List<string>();
        public List<string> nonconsumableProductsIds = new List<string>();
        public List<string> subscriptionsProductsIds = new List<string>();

        //// Apple App Store-specific product identifier for the subscription product.
        //private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";

        //// Google Play Store-specific product identifier subscription product.
        //private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";

        [Space]
        [SerializeField]
        string notAvailablePrice = "N/A";

        [Header("Events")]
        [SerializeField]
        UnityEvent onStartPurchase = new UnityEvent();
        [SerializeField]
        UnityEvent onPurchaseComplete = new UnityEvent();
        [SerializeField]
        UnityEvent onPurchaseFailed = new UnityEvent();
        [SerializeField]
        UnityEvent onRestoreFailed = new UnityEvent();

        [Header("Dev. test")]
        [SerializeField]
        string test_ProductId;
#if ENABLE
        public static bool IsInPurchaseProcess { get; private set; } = false;

        public static string LastInitiatedProductId
        {
            get
            {
                return lastInitiatedProductId;
            }

            private set
            {
                lastInitiatedProductId = value;
            }
        }


        static string lastInitiatedProductId;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                DestroyImmediate(this);
                return;
            }


            // If we haven't set up the Unity Purchasing reference
            if (m_StoreController == null)
            {
                // Begin to configure our connection to Purchasing
                InitializePurchasing();
            }
        }

        public static void ClearLastLastInitiatedProductId()
        {
            LastInitiatedProductId = "";
        }

        public void InitializePurchasing()
        {
            // If we have already connected to Purchasing ...
            if (IsInitialized())
            {
                // ... we are done here.
                return;
            }

            // Create a builder, first passing in a suite of Unity provided stores.
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            // Add a product to sell / restore by way of its identifier, associating the general identifier
            // with its store-specific identifiers.
            foreach (var item in consumableProductsIds)
            {
                builder.AddProduct(item, ProductType.Consumable);
            }
            // Continue adding the non-consumable product.
            foreach (var item in nonconsumableProductsIds)
            {
                builder.AddProduct(item, ProductType.NonConsumable);
            }

            //And finish adding the subscription product. Notice this uses store-specific IDs, illustrating
            // if the Product ID was configured differently between Apple and Google stores. Also note that
            // one uses the general kProductIDSubscription handle inside the game -the store - specific IDs
            //  must only be referenced here. 
            //builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs(){
            //    { kProductNameAppleSubscription, AppleAppStore.Name },
            //    { kProductNameGooglePlaySubscription, GooglePlay.Name },
            //});
            foreach (var item in subscriptionsProductsIds)
            {
                builder.AddProduct(item, ProductType.Subscription);
            }

            // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
            // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
            UnityPurchasing.Initialize(this, builder);
        }


        public bool IsInitialized()
        {
            // Only say we are initialized if both the Purchasing references are set.
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }


        public void BuyProduct(string productId)
        {
            ///
            IsInPurchaseProcess = true;
            LastInitiatedProductId = productId;

            ///
            onStartPurchase.Invoke();

            // Buy the consumable product using its general identifier. Expect a response either 
            // through ProcessPurchase or OnPurchaseFailed asynchronously.
            BuyProductID(productId);
        }


        void BuyProductID(string productId)
        {
            // If Purchasing has been initialized ...
            if (IsInitialized())
            {
                // ... look up the Product reference with the general product identifier and the Purchasing 
                // system's products collection.
                Product product = m_StoreController.products.WithID(productId);

                // If the look up found a product for this device's store and that product is ready to be sold ... 
                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                    // asynchronously.
                    m_StoreController.InitiatePurchase(product);
                }
                // Otherwise ...
                else
                {
                    ///
                    IsInPurchaseProcess = false;

                    ///
                    onPurchaseFailed.Invoke();

                    ///
                    if (OnPurchaseFailedEvents != null)
                    {
                        OnPurchaseFailedEvents();
                    }
                    // ... report the product look-up failure situation  
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            // Otherwise ...
            else
            {
                // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
                // retrying initiailization.
                Debug.Log("BuyProductID FAIL. Not initialized.");

                ///
                IsInPurchaseProcess = false;

                ///
                onPurchaseFailed.Invoke();

                ///
                if (OnPurchaseFailedEvents != null)
                {
                    OnPurchaseFailedEvents();
                }
            }
        }
#endif


        // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
        // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
        public void RestorePurchases()
        {
#if ENABLE
            ///
            LastInitiatedProductId = "";

            // If Purchasing has not yet been set up ...
            if (!IsInitialized())
            {
                // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
                Debug.Log("RestorePurchases FAIL. Not initialized.");

                ///
                IsInPurchaseProcess = false;

                ///
                onRestoreFailed.Invoke();

                ///
                if (OnRestoreFailedEvent != null)
                {
                    OnRestoreFailedEvent();
                }
                return;
            }

            // If we are running on an Apple device ... 
            if (Application.platform == RuntimePlatform.IPhonePlayer ||
                Application.platform == RuntimePlatform.OSXPlayer)
            {
                // ... begin restoring purchases
                Debug.Log("RestorePurchases started ...");

                // Fetch the Apple store-specific subsystem.
                var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
                // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
                // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
                apple.RestoreTransactions((result) =>
                {
                    // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                    // no purchases are available to be restored.
                    Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");

                    if (!result)
                    {
                        ///
                        IsInPurchaseProcess = false;

                        ///
                        onRestoreFailed.Invoke();

                        ///
                        if (OnRestoreFailedEvent != null)
                        {
                            OnRestoreFailedEvent();
                        }
                    }
                });
            }
            // Otherwise ...
            else
            {
                ///
                IsInPurchaseProcess = false;

                ///
                onRestoreFailed.Invoke();

                ///
                if (OnRestoreFailedEvent != null)
                {
                    OnRestoreFailedEvent();
                }
                // We are not running on an Apple device. No work is necessary to restore purchases.
                Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
            }
#endif
        }


        //  
        // --- IStoreListener
        //

#if ENABLE
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            // Purchasing has succeeded initializing. Collect our Purchasing references.
            Debug.Log("OnInitialized: PASS");

            // Overall Purchasing system, configured with products for this application.
            m_StoreController = controller;
            // Store specific subsystem, for accessing device-specific store features.
            m_StoreExtensionProvider = extensions;

            if (OnInitializedEvents != null)
            {
                OnInitializedEvents();
            }
        }


        public void OnInitializeFailed(InitializationFailureReason error)
        {
            // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
            Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
        }


        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            ///
            IsInPurchaseProcess = false;

            ///
            onPurchaseComplete?.Invoke();

            ///
            if (IsPurchaseValid(args))
            {
                OnPurchaseCompleted?.Invoke(args.purchasedProduct.definition.id, args.purchasedProduct);
            }

            // Return a flag indicating whether this product has completely been received, or if the application needs 
            // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
            // saving purchased products to the cloud, and when that save is delayed. 
            return PurchaseProcessingResult.Complete;
        }

        bool IsPurchaseValid(PurchaseEventArgs args)
        {
            bool validPurchase = true; // Presume valid for platforms with no R.V.

            // Unity IAP's validation logic is only included on these platforms.
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX
            // Prepare the validator with the secrets we prepared in the Editor
            // obfuscation window.
            //var validator = new CrossPlatformValidator(GooglePlayTangle.Data(),
            //    AppleTangle.Data(), Application.identifier);

            //try
            //{
            //    // On Google Play, result has a single product ID.
            //    // On Apple stores, receipts contain multiple products.
            //    var result = validator.Validate(args.purchasedProduct.receipt);
            //    // For informational purposes, we list the receipt(s)
            //    Debug.Log("Receipt is valid. Contents:");
            //    foreach (IPurchaseReceipt productReceipt in result)
            //    {
            //        Debug.Log(productReceipt.productID);
            //        Debug.Log(productReceipt.purchaseDate);
            //        Debug.Log(productReceipt.transactionID);
            //    }
            //}
            //catch (IAPSecurityException)
            //{
            //    Debug.Log("Invalid receipt, not unlocking content");
            //    validPurchase = false;
            //}
#endif

#if UNITY_EDITOR
            return true;
#else
            return validPurchase; 
#endif
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
            // this reason with the user to guide their troubleshooting actions.
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));

            ///
            IsInPurchaseProcess = false;

            ///
            onPurchaseFailed.Invoke();

            ///
            if (OnPurchaseFailedEvents != null)
            {
                OnPurchaseFailedEvents();
            }
        }

        public string GetProductDisplayPrice(string productId)
        {
            try
            {
                return m_StoreController.products.WithID(productId).metadata.localizedPriceString;
            }
            catch (Exception)
            {
                return notAvailablePrice;
            }
        }

        public Product GetProduct(string productId)
        {
            try
            {
                return m_StoreController.products.WithID(productId);
            }
            catch
            {
                return null;
            }
        }

        public string GetProductDisplayName(string productId)
        {
            return m_StoreController.products.WithID(productId).metadata.localizedTitle;
        }

        [ContextMenu("TriggerProductPurchase")]
        void TriggerProductPurchase()
        {
            ///
            IsInPurchaseProcess = false;

            ///            
            onPurchaseComplete.Invoke();

            ///
            if (OnPurchaseCompleted != null)
            {
                OnPurchaseCompleted(test_ProductId, null);
            }
        }
#endif
    }
}
