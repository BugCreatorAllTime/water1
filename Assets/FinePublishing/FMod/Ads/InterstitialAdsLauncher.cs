using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public class InterstitialAdsLauncher : MonoBehaviour
    {
        private const string INTER_SHOW_COUNT_KEY = "INTER_SHOW_COUNT_KEY";


        [SerializeField] int nSkip = 2;

        public void TryShow()
        {
            AddShownTime();
            if (GetShownTime() <= nSkip)
            {
                Debug.LogError("Skip inter because n Shown time = " + GetShownTime() + " threshold = " + nSkip);
                return;
            }

            Debug.LogError("Show inter");

            //InterstitialAds.ShowQuick(null);
            Ads_Controller.Show_InterstitialAds();
        }

        int GetShownTime()
        {
            return PlayerPrefs.GetInt(INTER_SHOW_COUNT_KEY, 0);
        }

        void AddShownTime()
        {
            int nShownTime = GetShownTime();
            nShownTime++;
            PlayerPrefs.SetInt(INTER_SHOW_COUNT_KEY, nShownTime);
            PlayerPrefs.Save();
        }
    }

}