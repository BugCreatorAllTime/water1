using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerAdsController : MonoBehaviour
{
    [SerializeField]
    FMod.BannerAdsPosition bannerAdsPosition = FMod.BannerAdsPosition.Bottom;

    public void TryHideBannerAds()
    {
        FMod.BannerAds.Hide();
    }

    public void TryShowBannerAds()
    {
        FMod.BannerAds.Show(bannerAdsPosition);
    }
}
