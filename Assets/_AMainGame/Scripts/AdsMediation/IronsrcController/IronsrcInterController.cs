using System.Collections;
using System.Collections.Generic;
using FMod;
using UnityEngine;
using static Ads_Controller;

public class IronsrcInterController : MonoBehaviour
{
    public const float InterDurationThreshold = 60;

    public static IronSource Agent => IronSource.Agent;

    [Header("Dummy")]
    [SerializeField] IronsrcDummyAds dummyAds;

    [Header("Debug")]
    [SerializeField] float lastTimeShowInter = 0;


    public void LoadInterstitial()
    {
        Agent.loadInterstitial();
    }


    bool IsInterPassDurationThreshold()
    {
        float duration = Time.realtimeSinceStartup - lastTimeShowInter;
        return duration >= InterDurationThreshold;
    }

    float InterNeedShowAfterSleepRate()
    {
        float duration = Time.realtimeSinceStartup - lastTimeShowInter;
        return Mathf.Max(1.0f, duration / (Mathf.Max(1.0f, InterDurationThreshold)));
    }


    public void ShowInterstitialAdsCheckTime()
    {
        if (IsInterPassDurationThreshold())
        {
            Debug.LogError("AdsController: Pass Check duration threshold for Interstitial");
            DoShowInterstitialAds();
        }
        else
        {
            Debug.LogError("AdsController: Interstitial not shown by duration threshold");
        }
    }

    public void DoShowInterstitialAds()
    {
#if UNITY_EDITOR
        dummyAds.DoShowInterAds();
        LogShowInterSuccess();
        return;
#endif

        if (Agent.isInterstitialReady())
        {
            Agent.showInterstitial();
            LogShowInterSuccess();
        }
        else
        {
            StartCoroutine(DelayToLoadInter());
            Debug.LogError("AdsController: cannot show inter ads by no ads available");
        }
    }


    #region track_log
    void LogShowInterSuccess()
    {
        LogTimeShowInter();
        AdsAnalytics.LogShowInterstitial();
    }

    void LogTimeShowInter()
    {
        lastTimeShowInter = Time.realtimeSinceStartup;
    }
    #endregion

    private void OnApplicationPause(bool pause)
    {
        LogTimeShowInter();
    }


    private void OnEnable()
    {
        // Add Interstitial Events
        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

        // Add Interstitial DemandOnly Events
        IronSourceEvents.onInterstitialAdReadyDemandOnlyEvent += InterstitialAdReadyDemandOnlyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedDemandOnlyEvent += InterstitialAdLoadFailedDemandOnlyEvent;
        IronSourceEvents.onInterstitialAdShowFailedDemandOnlyEvent += InterstitialAdShowFailedDemandOnlyEvent;
        IronSourceEvents.onInterstitialAdClickedDemandOnlyEvent += InterstitialAdClickedDemandOnlyEvent;
        IronSourceEvents.onInterstitialAdOpenedDemandOnlyEvent += InterstitialAdOpenedDemandOnlyEvent;
        IronSourceEvents.onInterstitialAdClosedDemandOnlyEvent += InterstitialAdClosedDemandOnlyEvent;

        // Add Rewarded Interstitial Events
        //IronSourceEvents.onInterstitialAdRewardedEvent += InterstitialAdRewardedEvent;
    }

    IEnumerator DelayToLoadInter()
    {
        yield return new WaitForSeconds(5.0f);
        LoadInterstitial();
    }

    #region Interstitial callback handlers

    void InterstitialAdReadyEvent()
    {
        Debug.Log("unity-script: I got InterstitialAdReadyEvent");
    }

    void InterstitialAdLoadFailedEvent(IronSourceError error)
    {
        Debug.Log("unity-script: I got InterstitialAdLoadFailedEvent, code: " + error.getCode() + ", description : " + error.getDescription());
        StartCoroutine(DelayToLoadInter());
    }

    void InterstitialAdShowSucceededEvent()
    {
        Debug.Log("unity-script: I got InterstitialAdShowSucceededEvent");
    }

    void InterstitialAdShowFailedEvent(IronSourceError error)
    {
        Debug.Log("unity-script: I got InterstitialAdShowFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription());
        LoadInterstitial();
    }

    void InterstitialAdClickedEvent()
    {
        Debug.Log("unity-script: I got InterstitialAdClickedEvent");
    }

    void InterstitialAdOpenedEvent()
    {
        Debug.Log("unity-script: I got InterstitialAdOpenedEvent");
    }

    void InterstitialAdClosedEvent()
    {
        Debug.Log("unity-script: I got InterstitialAdClosedEvent");
        LogTimeShowInter();
        LoadInterstitial();
    }

    void InterstitialAdRewardedEvent()
    {
        Debug.Log("unity-script: I got InterstitialAdRewardedEvent");
    }

    /************* Interstitial DemandOnly Delegates *************/

    void InterstitialAdReadyDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got InterstitialAdReadyDemandOnlyEvent for instance: " + instanceId);
    }

    void InterstitialAdLoadFailedDemandOnlyEvent(string instanceId, IronSourceError error)
    {
        Debug.Log("unity-script: I got InterstitialAdLoadFailedDemandOnlyEvent for instance: " + instanceId + ", error code: " + error.getCode() + ",error description : " + error.getDescription());
    }

    void InterstitialAdShowFailedDemandOnlyEvent(string instanceId, IronSourceError error)
    {
        Debug.Log("unity-script: I got InterstitialAdShowFailedDemandOnlyEvent for instance: " + instanceId + ", error code :  " + error.getCode() + ",error description : " + error.getDescription());
    }

    void InterstitialAdClickedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got InterstitialAdClickedDemandOnlyEvent for instance: " + instanceId);
    }

    void InterstitialAdOpenedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got InterstitialAdOpenedDemandOnlyEvent for instance: " + instanceId);
    }

    void InterstitialAdClosedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got InterstitialAdClosedDemandOnlyEvent for instance: " + instanceId);
    }

    void InterstitialAdRewardedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got InterstitialAdRewardedDemandOnlyEvent for instance: " + instanceId);
    }


    #endregion
}
