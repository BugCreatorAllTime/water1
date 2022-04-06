#if USE_STANDALONE_ADMOB
//using GoogleMobileAds.Api; 
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public abstract class AdmobBannerBase : MonoBehaviour
    {
#if USE_STANDALONE_ADMOB
        //[SerializeField]
        //AdPosition adPosition;

        //BannerView bannerView;

        //protected abstract string GetBannerUnitId();

        //protected void Show()
        //{
        //    ///
        //    if (!Ads.EnableInterAndBanner)
        //    {
        //        return;
        //    }

        //    ///
        //    if (bannerView == null)
        //    {
        //        RequestBanner();
        //        bannerView.Show();
        //        Debug.LogError("Request then show banner");
        //    }
        //    else
        //    {
        //        bannerView.Show();
        //        Debug.LogError("Requested. Now show banner");
        //    }
        //}

        //protected void Hide()
        //{
        //    if (bannerView != null)
        //    {
        //        bannerView.Hide();
        //    }
        //}

        //private void OnDestroy()
        //{
        //    if (bannerView != null)
        //    {
        //        bannerView.Destroy();
        //    }
        //}

        //private void RequestBanner()
        //{
        //    bannerView = new BannerView(GetBannerUnitId(), AdSize.Banner, adPosition);
        //    AdRequest request = new AdRequest.Builder().Build();
        //    bannerView.LoadAd(request);
        //    bannerView.OnAdLoaded += BannerView_OnAdLoaded;
        //}

        //void BannerView_OnAdLoaded(object sender, System.EventArgs e)
        //{
        //    Ads.LogAdsImpression();
        //    Ads.LogBannerImpression();
        //} 
#endif
    }

}