using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinePublishingDemo : MonoBehaviour
{
    public void TryShowInter()
    {
        FMod.InterstitialAds.ShowQuick(null);
    }

    public void TryShowRewarded()
    {
        FMod.RewardedAds.ShowQuick(null);
    }

    public void RemoveAds()
    {
        FMod.Ads.EnableInterAndBanner = false;
    }

    public void EnableAds()
    {
        FMod.Ads.EnableInterAndBanner = true;
    }
}
