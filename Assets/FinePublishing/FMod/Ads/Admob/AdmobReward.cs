using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if USE_STANDALONE_ADMOB
//using GoogleMobileAds.Api;
#endif

namespace FMod
{
    public class AdmobReward : MonoBehaviour, IRewardedAds
    {
        public static event System.Action OnAdmobRewardedAdsClosed = delegate { };

        [SerializeField]
        MString unitId = new MString();


//#if USE_STANDALONE_ADMOB
        //private RewardedAd rewardedAd;

        //RewardedAdsClosedCallback onClosedCallback = null;

        //public string AdId => AdsIds.AdmobRewarded;

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
        //        if (rewardedAd == null)
        //        {
        //            return false;
        //        }

        //        ///
        //        return rewardedAd.IsLoaded();
        //    }
        //}

        //public bool IsMonetizable => true;

        //void RequestNewAds()
        //{

        //    rewardedAd = new RewardedAd(unitId);

        //    rewardedAd.OnAdClosed += RewardBasedVideoAd_OnAdClosed;
        //    rewardedAd.OnUserEarnedReward += RewardBasedVideoAd_OnAdRewarded;
        //    rewardedAd.OnAdFailedToLoad += RewardedAd_OnAdFailedToLoad;

        //    ///
        //    AdRequest request = new AdRequest.Builder().Build();
        //    rewardedAd.LoadAd(request);

        //    ///
        //    Debug.Log("Request new rewarded ads");
        //}

        //private void RewardedAd_OnAdFailedToLoad(object sender, AdErrorEventArgs e)
        //{
        //    Invoke("RequestNewAds", 5);

        //    ///
        //    Debug.Log("Failed to Request new rewarded ads");
        //    Debug.Log(e.Message);
        //}

        //public void Awake()
        //{
        //    RequestNewAds();
        //}

        //private void RewardBasedVideoAd_OnAdRewarded(object sender, Reward e)
        //{
        //    if (onClosedCallback != null)
        //    {
        //        onClosedCallback(true);
        //    }
        //}

        //private void RewardBasedVideoAd_OnAdClosed(object sender, System.EventArgs e)
        //{
        //    ///
        //    OnAdmobRewardedAdsClosed();

        //    ///
        //    RequestNewAds();
        //}

        //public void Show(RewardedAdsClosedCallback onClosedCallback)
        //{
        //    this.onClosedCallback = onClosedCallback;
        //    rewardedAd.Show();
        //}
//#else
        public bool IsAvailable => throw new System.NotImplementedException();

        public bool IsMonetizable => throw new System.NotImplementedException();

        public string AdId => throw new System.NotImplementedException();

        public void Show(RewardedAdsClosedCallback onClosedCallback)
        {
            throw new System.NotImplementedException();
        }

//#endif
    }

}