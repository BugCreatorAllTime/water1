using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InAppPurchaseResultHandler : MonoBehaviour
{
    HashSet<string> transactionIds = new HashSet<string>();

    [SerializeField]
    UnityEvent onPurchaseSucceeded;
    [SerializeField]
    UnityEvent onRemoveAds;

    public void Start()
    {
#if UNITY_PURCHASING && !UNITY_STANDALONE
        FMod.Purchaser.Instance.OnPurchaseCompleted += Instance_OnPurchaseCompleted;
#endif
    }

    private void Instance_OnPurchaseCompleted(string productId)
    {
#if UNITY_PURCHASING && !UNITY_STANDALONE
        ///
        if (string.IsNullOrEmpty(productId))
        {
            return;
        }

        ///
        if (
            productId == FMod.Purchaser.LastInitiatedProductId
            && product != null
            && !transactionIds.Contains(product.transactionID)
            && product.hasReceipt
            && ReporterUISwitcher.LockedUI
            )
        { 
            ///
            FMod.Purchaser.ClearLastLastInitiatedProductId();
            transactionIds.Add(product.transactionID);

            ///
            onPurchaseSucceeded?.Invoke();
        }

        ///
        switch (productId)
        {
            case GameConst.IAPItem_RemoveAds:
                RemoveAds();
                break;
            case GameConst.IAPItem_CoinPack1:
                Entry.Instance.playerDataObject.Data.AddCoins(3000);
                break;
            case GameConst.IAPItem_CoinPack2:
                Entry.Instance.playerDataObject.Data.AddCoins(6000);
                break;
            case GameConst.IAPItem_CoinPack3:
                Entry.Instance.playerDataObject.Data.AddCoins(10000);
                break;
            default:
                break;
        }
#endif
    }

    private void RemoveAds()
    {
        try
        {
            //AdsController.HideBanner();
        }
        catch (System.Exception ex)
        {
        }
        
        FMod.Ads.EnableInterAndBanner = false;
        onRemoveAds?.Invoke();
    }
}
