using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IronsrcDummyAds : MonoBehaviour
{
    [SerializeField] RectTransform interAds;
    [SerializeField] Button interCloseBtn;

    [SerializeField] RectTransform rewardAds;
    [SerializeField] Button rewardCloseBtn;
    [SerializeField] Button rewardWatchedBtn;

    [Header("Debug on Editor")]
    [SerializeField] bool rewardedVideoAvailable = false;
    [SerializeField] bool rewardGot = true;
    [SerializeField] bool interAvailable = false;


    private void Start()
    {
        interCloseBtn.onClick.AddListener(() =>
        {
            interAds.gameObject.SetActive(false);
            gameObject.SetActive(false);
        });
    }


    public bool IsInterAdsAvailable()
    {
        return interAvailable;
    }

    public void DoShowInterAds()
    {
        gameObject.SetActive(true);
        interAds.gameObject.SetActive(true);
    }

    public bool IsRewardedVideoAdsAvailable()
    {
        return rewardedVideoAvailable;
    }

    public void DoPlayRewardedVideoAds(Action<bool> callback)
    {
        gameObject.SetActive(true);
        rewardAds.gameObject.SetActive(true);
        rewardCloseBtn.onClick.RemoveAllListeners();
        rewardCloseBtn.onClick.AddListener(() =>
        {
            rewardAds.gameObject.SetActive(false);
            callback?.Invoke(false);
            gameObject.SetActive(false);
        });
        rewardWatchedBtn.onClick.RemoveAllListeners();
        rewardWatchedBtn.onClick.AddListener(() =>
        {
            rewardAds.gameObject.SetActive(false);
            callback?.Invoke(true);
            gameObject.SetActive(false);
        });
    }
}
