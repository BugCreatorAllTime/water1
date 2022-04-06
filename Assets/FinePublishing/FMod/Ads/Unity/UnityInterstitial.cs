#if UNITY_ADVERTISEMENTS
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS
using UnityEngine.Advertisements;
#endif

namespace FMod
{
    public class UnityInterstitial : IInterstitialAds
    {
        const string PlacementId = "video";

        public string AdId => AdsIds.UnityInterstitial;

        public bool IsAvailable
        {
            get
            {
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS
                ///
                if (!UnityAdsConfig.UseUnityAds)
                {
                    return false;
                }

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

        public UnityInterstitial()
        {
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS
            ///
            UnityAdsConfig.TryInitialize();
#endif
        }        

        public void Show(Action onClosedCallback)
        {
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS
            ShowOptions showOptions = new ShowOptions()
            {
                resultCallback = (ShowResult rs) =>
                  {
                      if (onClosedCallback != null)
                      {
                          onClosedCallback();
                      }
                  }
            };
            Advertisement.Show(PlacementId, showOptions);
#endif
        }
    }

}
#endif