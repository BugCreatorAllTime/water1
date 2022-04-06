using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockupAdsInterstitial : FMod.IInterstitialAds
{
    public bool IsAvailable => MockupAdsManager.Instance != null ? MockupAdsManager.Instance.IsInterAvailable : false;

    public string AdId => "MockupAdsInterstitial";

    public void Show(Action onClosedCallback)
    {
        ///
        if (!IsAvailable)
        {
            throw new System.Exception();
        }

        ///
        MockupAdsManager.Instance.ShowInterstitial(onClosedCallback);
    }
}
