using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public static class BannerAds
    {
        static IBannerAds bannerAds;

        static bool inited = false;

        static void Init()
        {
            ///
            TryGetBannerIfNeeded();

            ///
            inited = true;
        }

        public static void TryInit()
        {
            if (!inited)
            {
                Init();
            }
        }


        static BannerAds()
        {
            TryInit();
        }

        public static void Show(BannerAdsPosition bannerAdsPosition)
        {
            ///
            if (!Ads.EnableInterAndBanner)
            {
                return;
            }

            ///
            TryGetBannerIfNeeded();

            ///
            bannerAds.Show(bannerAdsPosition);

            ///
            AdsAnalytics.LogShowBanner(bannerAdsPosition);
        }

        public static void Hide()
        {
            ///
            TryGetBannerIfNeeded();

            ///
            bannerAds.Hide();
        }

        static void TryGetBannerIfNeeded()
        {
            if (bannerAds == null && AdmobAds.Instance != null)
            {
                bannerAds = AdmobAds.Instance.Banner;
            }
        }
    }

}