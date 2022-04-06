#define USE_TENJIN_ANDROID

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InitTenjin : MonoBehaviour
{
#if UNITY_ANDROID
    const string ApiKey = "Y4SQ6VKLP7SJE71GYFYES8QLD5ZWT1YQ"; // This is key for IBridge's partners Get it: https://www.tenjin.io/dashboard/organizations  
#elif UNITY_IOS
    const string ApiKey = "Y4SQ6VKLP7SJE71GYFYES8QLD5ZWT1YQ";
#else
    const string ApiKey = null;
#endif

    public static BaseTenjin TenjinInstance { get; private set; }

    static bool inited = false;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_IOS || (UNITY_ANDROID && USE_TENJIN_ANDROID)

        ///
        if (inited)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(ApiKey))
        {
            return;
        }

        TenjinInstance = Tenjin.getInstance(ApiKey);

        // Connect
        string deeplink = null;
        if (string.IsNullOrWhiteSpace(deeplink))
        {
            TenjinInstance.Connect();
            Debug.Log("Connected tenjin, no deep link");
        }
        else
        {
            TenjinInstance.Connect(deeplink);
            Debug.LogFormat("Connected tenjin, deep link: {0}", deeplink);
        }

        ///
        inited = true;
#endif
    }

   /* public static void LogPurchase(Product purchasedProduct)
    {
        ///
        if (string.IsNullOrWhiteSpace(ApiKey))
        {
            return;
        }

        ///
#if !UNITY_STANDALONE && USE_TENJIN_ANDROID
        var price = purchasedProduct.metadata.localizedPrice;
        double lPrice = decimal.ToDouble(price);
        var currencyCode = purchasedProduct.metadata.isoCurrencyCode;

        var wrapper = (Dictionary<string, object>)MiniJson.JsonDecode(purchasedProduct.receipt);
        if (null == wrapper)
        {
            return;
        }

        var payload = (string)wrapper["Payload"]; // For Apple this will be the base64 encoded ASN.1 receipt
        var productId = purchasedProduct.definition.id;
#endif

        ///
#if UNITY_ANDROID && USE_TENJIN_ANDROID

        var gpDetails = (Dictionary<string, object>)MiniJson.JsonDecode(payload);
        var gpJson = (string)gpDetails["json"];
        var gpSig = (string)gpDetails["signature"];

        LogAndroidPurchase(productId, currencyCode, 1, lPrice, gpJson, gpSig);

#elif UNITY_IOS

        var transactionId = purchasedProduct.transactionID;

        LogIosPurchase(productId, currencyCode, 1, lPrice, transactionId, payload);

#endif

    }*/

    public static void LogCompletedTutorial()
    {
        ///
        if (string.IsNullOrWhiteSpace(ApiKey))
        {
            return;
        }

        ///
#if !UNITY_STANDALONE
        TenjinInstance.SendEvent("CompletedTutorial");
#endif
    }

    public static void LogEngaged()
    {
        ///
        if (string.IsNullOrWhiteSpace(ApiKey))
        {
            return;
        }

        ///
#if !UNITY_STANDALONE
        TenjinInstance.SendEvent("Engaged");
#endif
    }

    public static void LogAchievedLevel(string level)
    {
        ///
        if (string.IsNullOrWhiteSpace(ApiKey))
        {
            return;
        }

        ///
#if !UNITY_STANDALONE
        //TenjinInstance.SendEvent("AchievedLevel", level);
#endif
    }


    static void LogAndroidPurchase(string ProductId, string CurrencyCode, int Quantity, double UnitPrice, string Receipt, string Signature)
    {
        ///
        if (string.IsNullOrWhiteSpace(ApiKey))
        {
            return;
        }

        ///
#if UNITY_ANDROID && USE_TENJIN_ANDROID
        BaseTenjin instance = Tenjin.getInstance(ApiKey);
        instance.Transaction(ProductId, CurrencyCode, Quantity, UnitPrice, null, Receipt, Signature);
#endif
    }

    static void LogIosPurchase(string ProductId, string CurrencyCode, int Quantity, double UnitPrice, string TransactionId, string Receipt)
    {
        ///
        if (string.IsNullOrWhiteSpace(ApiKey))
        {
            return;
        }

        ///
#if !UNITY_STANDALONE
        BaseTenjin instance = Tenjin.getInstance(ApiKey);
        instance.Transaction(ProductId, CurrencyCode, Quantity, UnitPrice, TransactionId, Receipt, null);
#endif
    }
}
