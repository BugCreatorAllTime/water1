using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMod;
#if USE_STANDALONE_ADMOB
//using GoogleMobileAds.Api; 
#endif
using UnityEngine.Events;

namespace FMod
{
    public class RandomInterstitialAds : MonoBehaviour
    {
        [SerializeField]
        List<WeightedMString> admobs = new List<WeightedMString>();

#if USE_STANDALONE_ADMOB
        //bool EnableAds
        //{
        //    get
        //    {
        //        return AdmobInterstitialController.EnableAds;
        //    }
        //}

        //static Dictionary<string, int> showsCount = new Dictionary<string, int>();

        //[SerializeField]
        //bool requestNewAdOnShow = true;
        //[SerializeField]
        //bool showOnLoaded = false;
        //[SerializeField]
        //int skipCount = 2;

        //[Header("Editor test")]
        //[SerializeField]
        //float loadSuccessRate = 0.2f;

        //[Space]
        //[SerializeField]
        //UnityEvent onFailedToShowAds = new UnityEvent();
        //[SerializeField]
        //UnityEvent onShowAds = new UnityEvent();
        //[SerializeField]
        //UnityEvent onClosedAds = new UnityEvent();

        //InterstitialAd interstitial;

        //string interstitialUnitId;

        //public void Start()
        //{
        //    Debug.LogFormat(gameObject, "fvs {0} {1}", gameObject.name, GetInstanceID());
        //    if (EnableAds)
        //    {
        //        RequestNewAds();
        //    }
        //}

        //public void OnDestroy()
        //{
        //    if (interstitial != null)
        //    {
        //        interstitial.OnAdLoaded -= Interstitial_OnAdLoaded;
        //        interstitial.OnAdClosed -= Interstitial_OnAdClosed;
        //    }
        //}

        //void RequestNewAds()
        //{
        //    DetermineAdsId();

        //    ///
        //    int count = 0;
        //    showsCount.TryGetValue(interstitialUnitId, out count);
        //    if (count < skipCount)
        //    {
        //        return;
        //    }

        //    ///
        //    interstitial = new InterstitialAd(interstitialUnitId);
        //    AdRequest request = new AdRequest.Builder().Build();
        //    interstitial.OnAdLoaded += Interstitial_OnAdLoaded;
        //    interstitial.OnAdClosed += Interstitial_OnAdClosed;
        //    interstitial.LoadAd(request);
        //}

        //void Interstitial_OnAdClosed(object sender, System.EventArgs e)
        //{
        //    onClosedAds.Invoke();
        //}

        //bool IsAdLoaded()
        //{
        //    if (Application.isEditor)
        //    {
        //        return Random.value < loadSuccessRate;
        //    }
        //    else
        //    {
        //        return interstitial.IsLoaded();
        //    }
        //}

        //private void Interstitial_OnAdLoaded(object sender, System.EventArgs e)
        //{
        //    if (this == null)
        //    {
        //        return;
        //    }

        //    if (showOnLoaded)
        //    {
        //        TryShowAds();
        //    }
        //}

        //public void TryShowAds()
        //{
        //    Debug.Log("TryShowAds");
        //    ///
        //    if (!EnableAds)
        //    {
        //        return;
        //    }

        //    ///
        //    int count = 0;
        //    showsCount.TryGetValue(interstitialUnitId, out count);
        //    count++;
        //    showsCount[interstitialUnitId] = count;
        //    if (count <= skipCount)
        //    {
        //        Debug.LogFormat("Skip ads, count={0}, skipCount={1}", count, skipCount);
        //        RequestNewAds();
        //        return;
        //    }

        //    ///
        //    if (IsAdLoaded())
        //    {
        //        Debug.Log("Showed Admob Insterstitial");
        //        onShowAds.Invoke();
        //        if (Application.isEditor)
        //        {
        //            onClosedAds.Invoke();
        //        }
        //        interstitial.Show();
        //        if (requestNewAdOnShow)
        //        {
        //            RequestNewAds();
        //        }
        //    }
        //    else
        //    {
        //        Debug.Log("Failed to show Admob Insterstitial");
        //        onFailedToShowAds.Invoke();
        //    }
        //}

        //public void TryShowAdsDelay(float delayTime)
        //{
        //    Invoke("TryShowAds", delayTime);
        //}

        //void DetermineAdsId()
        //{
        //    interstitialUnitId = WeightedMString.GetFromList(admobs).value;
        //    interstitialUnitId = interstitialUnitId.Trim();
        //} 
#endif
    }
}
