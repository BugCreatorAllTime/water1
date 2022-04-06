using System;
using System.Collections;
using System.Collections.Generic;
using FMod;
using UnityEngine;

public class Ads_Mediation_Ironsource : MonoBehaviour
{
    [Header("app key: ")]
    [SerializeField] string androidAppKey;
    [SerializeField] string iosAppKey;

    [Header("Ad unit: ")]
    [SerializeField] IronsrcBannerController banner;
    [SerializeField] IronsrcInterController inter;
    [SerializeField] IronsrcRewardedVideoController rewardedVideo;

    bool mediationInited = false;

    string appKey
    {
        get
        {
#if UNITY_ANDROID
                return androidAppKey;
#else
            return iosAppKey;
#endif
        }
    }

    private void Start()
    {
        Debug.LogError("Ironsrc_Mediation: IronSource.Agent.validateIntegration");
        IronSource.Agent.validateIntegration();

        Debug.Log("Ironsrc_Mediation: unity version" + IronSource.unityVersion());
        
        //IronSource.Agent.validateIntegration();
        //IronSource.Agent.setAdaptersDebug(true);
        Init_IronMediation();
        
    }

        public void Init_IronMediation()
    {
        if (mediationInited)
        {
            return;
        }
        mediationInited = true;

        // SDK init
        IronSource.Agent.init(appKey);
        Debug.Log("Ironsrc_Mediation: IronSource.Agent.init");
    }

    public void Allow_LoadBanner()
    {
        banner.Load_Banner();
    }

    public void Hide_Banner()
    {
        IronsrcBannerController.DestroyBanner();
    }

    #region inter
    public void StartLoadInterstitial()
    {
        inter.LoadInterstitial();
    }

    public void Show_InterstitialAds()
    {
        inter.DoShowInterstitialAds();
    }

    public void Show_InterstitialAdsCheckTime()
    {
        inter.ShowInterstitialAdsCheckTime();
    }
    #endregion


    #region rewarded_video
    public bool IsRewardedVideoAdsAvailable()
    {
        return rewardedVideo.IsRewardedVideoAdsAvailable();
    }

    public void Load_RewardedVideoAds()
    {
        rewardedVideo.StartLoadRewardedVideoAds();
    }

    public void PlayRewardedVideoAds(Action<bool> callback)
    {
        rewardedVideo.DoPlayRewardedVideoAds((result) =>
        {
            if (callback != null)
            {
                callback(result);
            }
            if (result)
            {
                AdsAnalytics.LogCompletedRewarded("");
            }
            
            Load_RewardedVideoAds();
        });
        AdsAnalytics.LogShowRewarded();
    }
    #endregion


    void OnApplicationPause(bool isPaused)
    {
        Debug.Log("unity-script: OnApplicationPause = " + isPaused);
        IronSource.Agent.onApplicationPause(isPaused);
    }
}