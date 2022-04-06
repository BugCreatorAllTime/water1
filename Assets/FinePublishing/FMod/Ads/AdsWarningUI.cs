using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public class AdsWarningUI : MonoBehaviour
    {
        [SerializeField]
        GameObject warningObject;

        int counter = 0;

        public void Awake()
        {
            ///
            InterstitialAds.OnShowAds += OnShowAds;
            InterstitialAds.OnClosedAds += OnClosedAds;

            ///
            RewardedAds.OnShowAds += OnShowAds;
            RewardedAds.OnClosedAds += (bool isCompleted) => { OnClosedAds(); };
        }

        void OnShowAds()
        {
            counter++;
            warningObject.SetActive(counter > 0);
        }

        void OnClosedAds()
        {
            counter--;
            warningObject.SetActive(counter > 0);
        }
    }

}