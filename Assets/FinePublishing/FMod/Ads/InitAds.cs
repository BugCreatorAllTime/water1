using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public class InitAds : MonoBehaviour
    {
        [SerializeField]
        bool initInterstitial = true;
        [SerializeField]
        bool initRewarded = true;
        [SerializeField]
        bool initBanner = false;

        public void Awake()
        {
            ///
            if (initInterstitial)
            {
                InterstitialAds.TryInit();
            }

            ///
            if (initRewarded)
            {
                RewardedAds.TryInit();
            }

            ///
            if (initBanner)
            {
                BannerAds.TryInit();
            }
        }

    }

}