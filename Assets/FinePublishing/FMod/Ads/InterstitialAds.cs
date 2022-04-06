using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FMod
{
    public static class InterstitialAds
    {
        const string IntervalFactorKey = "IntervalFactorKey";

        const float DefaultInterval = 25.0f;

        static List<IInterstitialAds> interstitialAds = null;

        public static event Action OnShowAds;
        public static event Action OnClosedAds;
        public static event Action OnCheckAvailability;

        /// <summary>
        /// Last shown ads
        /// </summary>
        public static IInterstitialAds LastActiveAds { get; private set; }

        private static float intervalFactor = 0.2f;
        public static float IntervalFactor
        {
            get
            {
                return intervalFactor;
            }

            set
            {
                intervalFactor = value;
                PlayerPrefs.SetFloat(IntervalFactorKey, value);
            }
        }

        static int showCount = 0;

        private static float interval;

        public static bool IsAvailable
        {
            get
            {
                ///
                if (OnCheckAvailability != null)
                {
                    OnCheckAvailability();
                }

                ///
                if (!Ads.EnableInterAndBanner)
                {
                    return false;
                }

                ///
                TryGetInterstitialIfNeeded();

                // Interval
                if ((DateTime.Now - lastTimeShowAds).TotalSeconds < Interval)
                {
                    return false;
                }

                ///
                if (interstitialAds == null)
                {
                    return false;
                }

                ///
                foreach (var item in interstitialAds)
                {
                    if (item.IsAvailable)
                    {
                        return true;
                    }
                }

                ///
                return false;
            }
        }

        public static bool IsAvailabledForced
        {
            get
            {
                ///
                if (OnCheckAvailability != null)
                {
                    OnCheckAvailability();
                }

                ///
                if (!Ads.EnableInterAndBanner)
                {
                    return false;
                }

                ///
                TryGetInterstitialIfNeeded();

                // Interval
                //if ((DateTime.Now - lastTimeShowAds).TotalSeconds < Interval)
                //{
                //    return false;
                //}

                ///
                if (interstitialAds == null)
                {
                    return false;
                }

                ///
                foreach (var item in interstitialAds)
                {
                    if (item.IsAvailable)
                    {
                        return true;
                    }
                }

                ///
                return false;
            }
        }

        public static float Interval
        {
            get
            {
                return interval * intervalFactor;
            }
        }

        public static bool IsShowing { get; private set; }

        public static DateTime lastTimeShowAds;

        static bool inited = false;

        static void Init()
        {
            ///
            interval = DefaultInterval;
            intervalFactor = PlayerPrefs.GetFloat(IntervalFactorKey, 1);
            lastTimeShowAds = DateTime.MinValue;
            TryGetInterstitialIfNeeded();

            ///
            RemoteSettings_Updated();
            RemoteSettings.Updated += RemoteSettings_Updated;

            ///
            RewardedAds.OnShowAds += RewardedAds_OnShowAds;

            ///
            inited = true;
        }

        private static void RewardedAds_OnShowAds()
        {
            //if (RewardedAds.LastActiveAds.IsMonetizable)
            //{
            //    lastTimeShowAds = DateTime.Now;
            //}            
        }

        static void RemoteSettings_Updated()
        {
            interval = RemoteSettings.GetFloat(RemoteKeys.InterAdsInterval, DefaultInterval);
        }

        public static void TryInit()
        {
            if (!inited)
            {
                Init();
            }
        }

        static InterstitialAds()
        {
            TryInit();
        }

        #region EDITOR
#if UNITY_EDITOR
        [UnityEditor.MenuItem("FH/Ads/Reset Interstitial Interal Time")]
        private static void ResetIntervalTime()
        {
            lastTimeShowAds = DateTime.MinValue;
        }
#endif 
        #endregion

        public static void Show(Action onClosedCallback)
        {
            ///
            float showTimeStamp = Time.realtimeSinceStartup;

            ///
            System.Action wrappedCallback = () =>
            {
                UnityThreadHelper.Instance.DispatchToUnityThread(
                    () =>
                    {
                        ///
                        IsShowing = false;

                        ///
                        lastTimeShowAds = DateTime.Now;

                        ///
                        if (onClosedCallback != null)
                        {
                            onClosedCallback();
                        }

                        ///
                        if (OnClosedAds != null)
                        {
                            OnClosedAds();
                        }

                        ///
                        AdsAnalytics.LogShowInterstitial();

                        ///
                        Ads.InterAdsWatchedCount++;
                        Ads.LogWatchedFullAds();
                    });
            };

            ///
            for (int i = 0; i < interstitialAds.Count; i++)
            {
                var ads = interstitialAds[i];
                if (ads.IsAvailable)
                {
                    lastTimeShowAds = DateTime.Now;

                    ///
                    ads.Show(wrappedCallback);

                    ///
                    IsShowing = true;

                    ///
                    LastActiveAds = ads;

                    ///
                    if (OnShowAds != null)
                    {
                        OnShowAds();
                    }

                    ///
                    break;
                }
            }
        }

        public static bool ShowQuickForced(Action onClosedCallback)
        {
            if (IsAvailabledForced)
            {
                Show(onClosedCallback);

                return true;
            }
            else
            {
                if (onClosedCallback != null)
                {
                    onClosedCallback();
                }

                return false;
            }
        }

        public static bool ShowQuick(Action onClosedCallback)
        {
            if (IsAvailable)
            {
                Show(onClosedCallback);

                return true;
            }
            else
            {
                if (onClosedCallback != null)
                {
                    onClosedCallback();
                }

                return false;
            }
        }

        static void TryGetInterstitialIfNeeded()
        {
            ///
            if (!Ads.EnableInterAndBanner)
            {
                return;
            }

            ///
            if (interstitialAds == null && AdmobAds.Instance != null)
            {
                interstitialAds = new List<IInterstitialAds>();
#if UNITY_EDITOR
                interstitialAds.Add(new MockupAdsInterstitial());
#endif
                interstitialAds.Add(AdmobAds.Instance.Interstitial);

            }
        }
    }
}