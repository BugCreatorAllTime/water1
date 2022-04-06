#if USE_STANDALONE_ADMOB
//using GoogleMobileAds.Api;
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public class AdmobInterstitial : MonoBehaviour, IInterstitialAds
    {
        [SerializeField]
        MString unitId = new MString();

//#if USE_STANDALONE_ADMOB
        //InterstitialAd interstitial;

        //System.Action onClosedCallback = null;

        //public string AdId => AdsIds.AdmobInterstitial;

        //public void Awake()
        //{
        //    if (Ads.EnableInterAndBanner)
        //    {
        //        RequestNewAds();
        //    }
        //}

        //public bool IsAvailable
        //{
        //    get
        //    {
        //        ///
        //        if (Application.isEditor)
        //        {
        //            return false;
        //        }

        //        ///
        //        if (interstitial == null)
        //        {
        //            return false;
        //        }

        //        ///
        //        return interstitial.IsLoaded();
        //    }
        //}

        //void RequestNewAds()
        //{
        //    interstitial = new InterstitialAd(unitId);
        //    AdRequest request = new AdRequest.Builder().Build();
        //    interstitial.OnAdLoaded += Interstitial_OnAdLoaded;
        //    interstitial.OnAdClosed += Interstitial_OnAdClosed;
        //    interstitial.OnAdFailedToLoad += Interstitial_OnAdFailedToLoad;
        //    interstitial.LoadAd(request);
        //}

        //private void Interstitial_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
        //{
        //    Debug.LogFormat("Failed to load Ads: {0}", e.Message);
        //    Invoke("RequestNewAds", 5);
        //}

        //void Interstitial_OnAdClosed(object sender, System.EventArgs e)
        //{
        //    if (onClosedCallback != null)
        //    {
        //        onClosedCallback();
        //    }
        //}

        //private void Interstitial_OnAdLoaded(object sender, System.EventArgs e)
        //{

        //}

        //public void Show(Action onClosedCallback)
        //{
        //    ///
        //    this.onClosedCallback = onClosedCallback;
        //    interstitial.Show();

        //    ///
        //    RequestNewAds();
        //}
//#else
        public bool IsAvailable => throw new NotImplementedException();

        public string AdId => throw new NotImplementedException();

        public void Show(Action onClosedCallback)
        {
            throw new NotImplementedException();
        }
//#endif
    }

}