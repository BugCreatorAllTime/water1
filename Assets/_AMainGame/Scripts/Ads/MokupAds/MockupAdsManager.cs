using FMod;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockupAdsManager : MonoBehaviour
{
    [SerializeField]
    private bool isInterAvailable = true;
    [SerializeField]
    private bool isRewardedAvailable = true;

    [Space]
    [SerializeField]
    private GameObject interObject;
    [SerializeField]
    private GameObject rewardedObject;

    public static MockupAdsManager Instance { get; private set; }

    public bool IsInterAvailable => isInterAvailable;
    public bool IsRewardedAvailable => isRewardedAvailable;

    private Action onClosedCallbackInter;
    private RewardedAdsClosedCallback onClosedCallbackRewarded;

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

        ///
        DontDestroyOnLoad(gameObject);
    }

    public void ShowInterstitial(Action onClosedCallback)
    {
        interObject.SetActive(true);
        onClosedCallbackInter = onClosedCallback;
    }

    public void ShowRewarded(RewardedAdsClosedCallback onClosedCallback)
    {
        rewardedObject.SetActive(true);
        onClosedCallbackRewarded = onClosedCallback;
    }

    public void OnCloseInter()
    {
        onClosedCallbackInter?.Invoke();
    }

    public void OnCloseRewarded(bool completed)
    {
        onClosedCallbackRewarded?.Invoke(completed);
    }
}
