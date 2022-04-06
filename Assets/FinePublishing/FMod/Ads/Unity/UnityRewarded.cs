#if UNITY_ADVERTISEMENTS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS
using UnityEngine.Advertisements;
#endif

namespace FMod
{
    public class UnityRewarded : IRewardedAds
    {
        const string PlacementId = "rewardedVideo";

        public string AdId => AdsIds.UnityRewarded;

        public bool IsMonetizable => true;

        public bool IsAvailable
        {
            get
            {
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS
                ///
                if (!Advertisement.isInitialized)
                {
                    return false;
                }

                ///
                return Advertisement.IsReady(PlacementId);
#else 
                return false;
#endif
            }
        }

        public UnityRewarded()
        {
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS
            UnityAdsConfig.TryInitialize();
#endif
        }

        public void Show(RewardedAdsClosedCallback onClosedCallback)
        {
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS
            ShowOptions showOptions = new ShowOptions()
            {
                resultCallback = (ShowResult rs) =>
                {
                    if (onClosedCallback != null)
                    {
                        onClosedCallback(rs == ShowResult.Finished);
                    }
                }
            };
            Advertisement.Show(PlacementId, showOptions);
#endif
        }
    }


} 
#endif