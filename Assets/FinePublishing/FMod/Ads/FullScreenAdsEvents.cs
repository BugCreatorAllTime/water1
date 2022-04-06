using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FMod
{
    public class FullScreenAdsEvents : MonoBehaviour
    {
        [SerializeField]
        bool monetizableRewardedAdsOnly = false;

        [Space]
        [SerializeField]
        UnityEvent onFullAdShowed;
        [SerializeField]
        UnityEvent onFullAdClosed;

        public void OnEnable()
        {
            InterstitialAds.OnShowAds += InterstitialAds_OnShowAds;
            InterstitialAds.OnClosedAds += InterstitialAds_OnClosedAds;
            RewardedAds.OnShowAds += RewardedAds_OnShowAds;
            RewardedAds.OnClosedAds += RewardedAds_OnClosedAds;
        }

        public void OnDisable()
        {
            InterstitialAds.OnShowAds -= InterstitialAds_OnShowAds;
            InterstitialAds.OnClosedAds -= InterstitialAds_OnClosedAds;
            RewardedAds.OnShowAds -= RewardedAds_OnShowAds;
            RewardedAds.OnClosedAds -= RewardedAds_OnClosedAds;
        }

        private void RewardedAds_OnClosedAds(bool isSuccessul)
        {
            ///
            if (monetizableRewardedAdsOnly)
            {
                if (!RewardedAds.LastActiveAds.IsMonetizable)
                {
                    return;
                }

            }

            ///
            onFullAdClosed?.Invoke();
        }

        private void RewardedAds_OnShowAds()
        {
            ///
            if (monetizableRewardedAdsOnly)
            {
                if (!RewardedAds.LastActiveAds.IsMonetizable)
                {
                    return;
                }

            }

            ///
            onFullAdShowed?.Invoke();
        }

        private void InterstitialAds_OnClosedAds()
        {
            onFullAdClosed?.Invoke();
        }

        private void InterstitialAds_OnShowAds()
        {
            onFullAdShowed?.Invoke();
        }


    }
}
