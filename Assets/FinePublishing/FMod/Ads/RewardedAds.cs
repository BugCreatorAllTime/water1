using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FMod
{
    public static class RewardedAds
    {
        static List<IRewardedAds> rewardedAds = null;

        public static event Action OnShowAds;
        public static event RewardedAdsClosedCallback OnClosedAds;

        static bool inited = false;

        static bool isAvailable = false;
        static bool isMonetizableAdsAvailable = false;

        /// <summary>
        /// Last shown ads
        /// </summary>
        public static IRewardedAds LastActiveAds { get; private set; }

        public static bool IsAvailable
        {
            get
            {
                ///
                TryGetRewardAdsIfNeeded();

                ///
                if (rewardedAds == null)
                {
                    return false;
                }

                ///
                if (isAvailable)
                {
                    return true;
                }

                ///
                foreach (var item in rewardedAds)
                {
                    if (item.IsAvailable)
                    {
                        isAvailable = true;
                        return true;
                    }
                }

                ///
                return false;
            }
        }

        public static bool IsMonetizableAdsAvailable
        {
            get
            {
                ///
                TryGetRewardAdsIfNeeded();

                ///
                if (rewardedAds == null)
                {
                    return false;
                }

                ///
                if (isMonetizableAdsAvailable)
                {
                    return true;
                }

                ///
                foreach (var item in rewardedAds)
                {
                    if (item.IsMonetizable && item.IsAvailable)
                    {
                        isMonetizableAdsAvailable = true;
                        return true;
                    }
                }

                ///
                return false;
            }
        }

        public static bool IsShowing { get; private set; }

        static void Init()
        {
            ///
            TryGetRewardAdsIfNeeded();

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

        static RewardedAds()
        {
            TryInit();
        }

        public static void Show(RewardedAdsClosedCallback onClosedCallback)
        {
            for (int i = 0; i < rewardedAds.Count; i++)
            {
                var ads = rewardedAds[i];
                if (ads.IsAvailable)
                {
                    ///
                    Show(ads, onClosedCallback);

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

        public static void ShowMonetizable(RewardedAdsClosedCallback onClosedCallback)
        {
            for (int i = 0; i < rewardedAds.Count; i++)
            {
                var ads = rewardedAds[i];
                if (ads.IsMonetizable && ads.IsAvailable)
                {
                    ///
                    Show(ads, onClosedCallback);

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

        static void TryGetRewardAdsIfNeeded()
        {
#if UNITY_ANDROID || UNITY_IOS
            if (rewardedAds == null && AdmobAds.Instance != null)
            {
                ///
                rewardedAds = new List<IRewardedAds>();
#if UNITY_EDITOR
                rewardedAds.Add(new MockupAdsRewarded());
#endif                
                rewardedAds.Add(AdmobAds.Instance.Rewarded);
            }
#else
            if (rewardedAds == null)
            {
                ///
                rewardedAds = new List<IRewardedAds>();              
            }
#endif
        }

        public static void ShowQuick(Action onCompletedCallBack)
        {
            if (IsAvailable)
            {
                Show((completed) =>
                {
                    if (completed && onCompletedCallBack != null)
                    {
                        onCompletedCallBack();
                    }
                });
            }
        }

        static void Show(IRewardedAds rewardedAds, RewardedAdsClosedCallback onClosedCallback)
        {
            ///
            float showTimeStamp = Time.realtimeSinceStartup;

            ///
            InterstitialAds.lastTimeShowAds = System.DateTime.Now;

            ///
            RewardedAdsClosedCallback wrappedCallback = (bool isCompleted) =>
            {
                UnityThreadHelper.Instance.DispatchToUnityThread(
                    () =>
                    {
                        ///
                        InterstitialAds.lastTimeShowAds = System.DateTime.Now;

                        ///
                        IsShowing = false;

                        ///
                        if (onClosedCallback != null)
                        {
                            onClosedCallback(isCompleted);
                        }

                        ///
                        if (OnClosedAds != null)
                        {
                            OnClosedAds(isCompleted);
                        }

                        ///
                        if (isCompleted)
                        {
                            ///
                            if (LastActiveAds.IsMonetizable)
                            {
                                Ads.RewardedAdsCompletedCount++;
                            }

                            ///
                            AdsAnalytics.LogCompletedRewarded(LastActiveAds.AdId);
                        }

                        ///
                        Ads.LogWatchedFullAds();
                    }
                    );
            };

            ///
            rewardedAds.Show(wrappedCallback);

            ///
            IsShowing = true;

            ///
            AdsAnalytics.LogShowRewarded();

            ///
            isAvailable = false;
            isMonetizableAdsAvailable = false;
        }
    }

}