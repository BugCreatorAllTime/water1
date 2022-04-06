using System.Collections;
using System.Collections.Generic;
using FMod;
using UnityEngine;

public class Ads_Controller : MonoBehaviour
{
    public enum ShowAdsAction
    {
        RewardedVideo = 0,
        InterShow = 1,
        InterNotShow = 2
    }

    private static Ads_Controller Instance;

    [SerializeField] Ads_Mediation_Ironsource ironSrc;

    Ads_Mediation_Ironsource _Mediation => ironSrc;

    static Ads_Mediation_Ironsource mediation => Instance._Mediation;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            GameObject.DestroyImmediate(gameObject);
            return;
        }
    }

    void Start()
    {
        
        //OnApplicationPause(false);
        StartCoroutine(Delay_ToLoadAds());
        
    }

    IEnumerator Delay_ToLoadAds()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(3.0f);

        if (CanControlAds && !IsNoAds)
        {
            mediation.StartLoadInterstitial();
            Allow_LoadBanner();
        }
        mediation.Load_RewardedVideoAds();
    }

    public static void InitMediation()
    {
        if (IsExist && Instance.ironSrc != null)
        {
            Instance.ironSrc.Init_IronMediation();
        }
    }

    static bool IsNoAds => !Ads.EnableInterAndBanner;

    static bool CanControlAds => IsExist;

    static bool IsExist => Instance != null;

    

    

    


    #region banner_ads
    public static void Allow_LoadBanner()
    {
        if (CanControlAds && !IsNoAds)
        {
            mediation.Allow_LoadBanner();
        }
    }

    public static void Hide_Banner()
    {
        if (CanControlAds)
        {
            mediation.Hide_Banner();
        }
    }
    #endregion
     #region inter_ads
    public static void Load_InterstitialAds()
    {
        if (CanControlAds && !IsNoAds)
        {
            mediation.StartLoadInterstitial();
        }
    }

    public static void Show_InterstitialAds()
    {
        if (CanControlAds && !IsNoAds)
        {
            mediation.Show_InterstitialAds();
        }
    }

    public static void Show_InterstitialAdsCheckTime()
    {
        if (CanControlAds && !IsNoAds)
        {
            mediation.Show_InterstitialAdsCheckTime();
        }
        else
        {
            Debug.LogError("AdsController: Interstitial not shown by No Ads or null data");
        }
    }
    #endregion
    #region rewarded video
    public static bool Is_RewardedVideoAvailable => IsExist? Instance.Is_RewardedVideoAdsAvailable() : false;

    bool Is_RewardedVideoAdsAvailable()
    {
        return mediation.IsRewardedVideoAdsAvailable();
    }

    public static void Load_RewardedVideoAds()
    {
        if (IsExist)
        {
            mediation.Load_RewardedVideoAds();
        }
    }

    public static void Play_RewardedVideoAds(System.Action<bool> callback)
    {
        if (IsExist && Is_RewardedVideoAvailable)
        {
            mediation.PlayRewardedVideoAds(callback);
        }
        else
        {
            if (callback != null)
            {
                callback(false);
            }
        }
    }
    #endregion
}
