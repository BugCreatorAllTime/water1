#if USE_STANDALONE_ADMOB
//using GoogleMobileAds.Api;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 


namespace FMod
{
    public class AdmobController : MonoBehaviour
    {
        public static AdmobController Instance;

        [Header("Reward ad")]
        [SerializeField]
        MString rewardUnitId;
        [SerializeField, Range(0, 1)]
        float maxRewardBonus = 0;

        [Space]
        [SerializeField]
        bool addTestDevice;
        [SerializeField]
        string[] testDevicesIds = new string[0];

#if USE_STANDALONE_ADMOB
//        RewardBasedVideoAd rewardBasedVideo;

//        public delegate void RewardAdCompletedHandler(double amount, string type);

//        RewardAdCompletedHandler rewardAdCompletetedCallback = null;

//        public event System.Action OnRewardAdAvailable;

//        public bool IsRewardAdAvailale
//        {
//            get
//            {
//#if UNITY_IOS
//                return rewardBasedVideo.IsLoaded();
//#else
//                return false;
//#endif
//            }
//        }

//        public void Awake()
//        {
//            if (Instance == null)
//            {
//                ///
//                Instance = this;
//                DontDestroyOnLoad(gameObject);

//                ///
//                rewardUnitId.TrimAllValues();
//                InitRewardAd();
//            }
//            else
//            {
//                Destroy(gameObject);
//            }
//        }

//        public void ShowRewardAd(RewardAdCompletedHandler completedCallback)
//        {
//            if (completedCallback == null)
//            {
//                throw new System.NullReferenceException();
//            }

//            rewardAdCompletetedCallback = completedCallback;

//            rewardBasedVideo.Show();
//        }

//        void InitRewardAd()
//        {
//            rewardBasedVideo = RewardBasedVideoAd.Instance;
//            rewardBasedVideo.OnAdRewarded += RewardBasedVideo_OnAdRewarded;
//            rewardBasedVideo.OnAdClosed += RewardBasedVideo_OnAdClosed;
//            rewardBasedVideo.OnAdLoaded += RewardBasedVideo_OnAdLoaded;

//            RequestRewardAd();
//        }

//        private void RewardBasedVideo_OnAdLoaded(object sender, System.EventArgs e)
//        {
//            if (OnRewardAdAvailable != null)
//            {
//                OnRewardAdAvailable();
//            }
//        }

//        private void RewardBasedVideo_OnAdClosed(object sender, System.EventArgs e)
//        {
//            RequestRewardAd();
//        }

//        void RewardBasedVideo_OnAdRewarded(object sender, Reward e)
//        {
//            if (rewardAdCompletetedCallback != null)
//            {
//                rewardAdCompletetedCallback(GetFinalReward(e.Amount), e.Type);
//            }
//        }

//        double GetFinalReward(double originalAmount)
//        {
//            float bonus = Random.Range(0, maxRewardBonus);
//            return originalAmount + originalAmount * bonus;
//        }

//        void RequestRewardAd()
//        {
//            AdRequest request = new AdRequest.Builder().Build();
//            if (addTestDevice)
//            {
//                request.TestDevices.AddRange(testDevicesIds);
//            }
//            rewardBasedVideo.LoadAd(request, rewardUnitId);
//        } 
#endif
    }

}