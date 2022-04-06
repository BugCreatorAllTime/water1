using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public static class AdsAnalytics
    {
        static int interstitialAdsCount = 0;

        public static void LogShowInterstitial()
        {
            MyAnalytics.Firebase_ShowInter();
        }

        public static void LogShowBanner(BannerAdsPosition bannerAdsPosition)
        {
            
        }

        public static void LogShowRewarded()
        {
            MyAnalytics.Firebase_ShowReward();
        }

        public static void LogCompletedRewarded(string adId)
        {
            MyAnalytics.Firebase_CompletedReward();
            MyAnalytics.Facebook_RewardShown();
        }

    }

}