using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPHelper : MonoBehaviour
{
    public void BuyRemoveAds()
    {
#if !UNITY_STANDALONE
        //FMod.Purchaser.Instance.BuyProduct(GameConst.IAPItem_RemoveAds);
#endif
    }

    public void BuyCoinPack1()
    {
#if !UNITY_STANDALONE
       // FMod.Purchaser.Instance.BuyProduct(GameConst.IAPItem_CoinPack1);
#endif
    }

    public void BuyCoinPack2()
    {
#if !UNITY_STANDALONE
        //FMod.Purchaser.Instance.BuyProduct(GameConst.IAPItem_CoinPack2);
#endif
    }

    public void BuyCoinPack3()
    {
#if !UNITY_STANDALONE
        //FMod.Purchaser.Instance.BuyProduct(GameConst.IAPItem_CoinPack3);
#endif
    }
}
