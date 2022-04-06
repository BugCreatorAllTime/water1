using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public class AppInfo : MonoBehaviour
    {
        const string AppleRatingUrl = @"itms-apps://itunes.apple.com/app/viewContentsUserReviews?id={0}";
        //const string AppleRatingUrl = @"https://itunes.apple.com/app/id{0}";
        const string GoogleRatingUrl = @"market://details?id={0}";

        const string AppleStorePageUrl = @"https://itunes.apple.com/app/id{0}";
        const string GoogleStorePageUrl = @"market://details?id={0}";

        public static AppInfo Instance { get; private set; }

        public string iTuneId = "1558817004";
        public string appHashTag = "#";

        public string GooglePackageName => Application.identifier;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public string GetRatingUrl()
        {
#if UNITY_IOS
            Debug.LogError("itune id = " + iTuneId);

            return string.Format(AppleRatingUrl, iTuneId);
#elif UNITY_ANDROID
            return string.Format(GoogleRatingUrl, GooglePackageName);
#else
            return null;
#endif
        }

        public string GetStorePageUrl()
        {
#if UNITY_IOS
            return string.Format(AppleStorePageUrl, iTuneId);
#elif UNITY_ANDROID
            return string.Format(GoogleStorePageUrl, GooglePackageName);
#else
            return null;
#endif
        }
    }

}