using System.Collections;
using System.Collections.Generic;
using FMod;
using UnityEngine;

public class MockupAdsRewarded : FMod.IRewardedAds
{
    public bool IsAvailable => MockupAdsManager.Instance != null ? MockupAdsManager.Instance.IsRewardedAvailable : false;

    public bool IsMonetizable => true;

    public string AdId => "MockupAdsRewarded";

    public void Show(RewardedAdsClosedCallback onClosedCallback)
    {
        ///
        if (!IsAvailable)
        {
            throw new System.Exception();
        }

        ///
        MockupAdsManager.Instance.ShowRewarded(onClosedCallback);
    }
}
