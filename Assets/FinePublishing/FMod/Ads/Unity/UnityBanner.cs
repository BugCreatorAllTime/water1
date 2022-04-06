#if UNITY_ADVERTISEMENTS
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

namespace FMod
{
    public class UnityBanner : IBannerAds
    {
        const float LoadAdsInterval = 60.0f;
        const float LoadAdsFromNullInterval = 30.0f;

        const string placementId = "banner";

        public event BannerAvailabilityChangedHandler OnAvailabilityChanged;

        bool isLoading = false;

        public bool IsAvailable
        {
            get
            {
#if UNITY_EDITOR
                return true;
#else
                ///
                var isAvailable = Advertisement.IsReady(placementId);

                ///
                if (!isAvailable)
                {
                    TryLoadAds();
                }

                ///
                return isAvailable;
#endif
            }
        }

        public string AdId => AdsIds.UnityBanner;

        public int ListId { get; set; }

        public UnityBanner()
        {
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS
            ///
            UnityAdsConfig.TryInitialize();

            ///
            if (Advertisement.isInitialized)
            {
                TryLoadAds();
            }
            else
            {
                UnityAdsConfig.OnUnityAdvertisementsInitialized += UnityAdsConfig_OnUnityAdvertisementsInitialized;
            }

            ///
            UnityThreadHelper.Instance.StartCoroutine(LoadAdsCoroutine());
#endif
        }

        void UnityAdsConfig_OnUnityAdvertisementsInitialized()
        {
            TryLoadAds();
        }

        private void TryLoadAds()
        {
            ///
            if (!Advertisement.isInitialized)
            {
                return;
            }

            ///
            if (isLoading)
            {
                return;
            }

            ///
            Advertisement.Banner.Load
                            (
                            placementId
                            ,
                            new BannerLoadOptions()
                            {
                                loadCallback = () =>
                                {
                                    ///
                                    OnAvailabilityChanged?.Invoke(this);

                                    ///
                                    isLoading = false;
                                },
                                errorCallback = (message) =>
                                {
                                    ///
                                    isLoading = false;
                                }
                            }
                            );

            ///
            isLoading = true;
        }

        public void Hide()
        {
            Advertisement.Banner.Hide();
        }

        public void Show(BannerAdsPosition position)
        {
            Advertisement.Banner.Show(placementId);
        }

        IEnumerator LoadAdsCoroutine()
        {
            while (true)
            {
                ///
                if (IsAvailable)
                {
                    yield return new WaitForSecondsRealtime(LoadAdsInterval);
                }
                else
                {
                    yield return new WaitForSecondsRealtime(LoadAdsFromNullInterval);
                }

                ///
                TryLoadAds();
            }
        }
    }

}
#endif