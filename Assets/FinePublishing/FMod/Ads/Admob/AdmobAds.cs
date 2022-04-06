using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public class AdmobAds : MonoBehaviour
    {
        public static AdmobAds Instance { get; private set; }

        [SerializeField]
        GameObject adsObjectsRoot;

        [Space]
        [SerializeField]
        AdmobBanner admobBanner;
        [SerializeField]
        AdmobInterstitial admobInterstitial;
        [SerializeField]
        AdmobReward admobReward;

        public IBannerAds Banner { get { return admobBanner; } }
        public IRewardedAds Rewarded { get { return admobReward; } }
        public IInterstitialAds Interstitial { get { return admobInterstitial; } }

        public void Awake()
        {
            ///
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            ///
            Instance = this;
            adsObjectsRoot.SetActive(true);
            DontDestroyOnLoad(gameObject);
        }
    }

}