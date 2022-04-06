using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public static class Ads
    {
        const string EnableInterAndBannerKey = "EnableInterAndBanner";
        const string RewardedAdsCompletedCountKey = "RewardedAdsCompletedCount";
        const string InterAdsWatchedCountKey = "InterAdsWatchedCount";
        const string TestInterAndBannerKey = "TestInterAndBanner";

        public static event System.Action OnRemovedAds;

        private static bool enableInterAndBanner;
        private static bool testInterAndBanner;

        private static int rewardedAdsCompletedCount;
        private static int interAdsWatchedCount;

        public static bool TestInterAndBanner
        {
            get
            {
                return testInterAndBanner;
            }

            set
            {
                ///
                testInterAndBanner = value;

                ///
                PlayerPrefs.SetInt(TestInterAndBannerKey, value ? 1 : 0);
                PlayerPrefs.Save();
            }
        }

        public static bool EnableInterAndBanner
        {
            get
            {
#if UNITY_IOS || UNITY_ANDROID
                return enableInterAndBanner || testInterAndBanner;
#else
                return false;
#endif
            }

            set
            {
                ///
                bool changed = enableInterAndBanner != value;

                ///
                enableInterAndBanner = value;

                ///
                PlayerPrefs.SetInt(EnableInterAndBannerKey, value ? 1 : 0);
                PlayerPrefs.Save();

                ///
                if (changed && !value)
                {
                    OnRemovedAds?.Invoke();
                }
            }
        }

        /// <summary>
        /// Monetizable ads only
        /// </summary>
        public static int RewardedAdsCompletedCount
        {
            get
            {
                return rewardedAdsCompletedCount;
            }

            set
            {
                rewardedAdsCompletedCount = value;

                ///
                PlayerPrefs.SetInt(RewardedAdsCompletedCountKey, rewardedAdsCompletedCount);
                PlayerPrefs.Save();
            }
        }

        public static int InterAdsWatchedCount
        {
            get => interAdsWatchedCount;
            set
            {
                ///
                interAdsWatchedCount = value;

                ///
                PlayerPrefs.SetInt(InterAdsWatchedCountKey, interAdsWatchedCount);
                PlayerPrefs.Save();
            }
        }

        public static bool IsShowingFullAds
        {
            get
            {
                return InterstitialAds.IsShowing || RewardedAds.IsShowing;
            }
        }

        static Ads()
        {
            enableInterAndBanner = PlayerPrefs.GetInt(EnableInterAndBannerKey, 1) == 1;
            testInterAndBanner = PlayerPrefs.GetInt(TestInterAndBannerKey, 0) == 1;
            rewardedAdsCompletedCount = PlayerPrefs.GetInt(RewardedAdsCompletedCountKey, 0);
            interAdsWatchedCount = PlayerPrefs.GetInt(InterAdsWatchedCountKey, 0);
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("FH/Ads/Remove Ads")]
        private static void RemoveAds()
        {
            EnableInterAndBanner = false;
            Debug.Log("Removed As");
        }
#endif

        // Watched interstitial or rewarded
        public static void LogWatchedFullAds()
        {
            
        }

        public static void LogBannerImpression()
        {
           
        }

        [System.Obsolete]
        public static void LogAdsImpression()
        {
            // Analytics.LogEvent("AdsImpression", 1);
        }
    }

}