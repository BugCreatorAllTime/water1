#if UNITY_ADVERTISEMENTS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS
using UnityEngine.Advertisements;
#endif

namespace FMod
{
    public static class UnityAdsConfig
    {
        public static event System.Action OnUnityAdvertisementsInitialized;

        public const string GameIdIos = "1622013";
        public const string GameIdAndroid = "1622012";

        static bool inited = false;

        public static string CurrentGameId
        {
            get
            {
#if UNITY_IOS
                return GameIdIos;
#elif UNITY_ANDROID
                return GameIdAndroid;
#else // for test sake
                return GameIdAndroid;
#endif
            }
        }

        public static bool UseUnityAds { get; private set; } = true;

        public static void TryInitialize()
        {
            ///
            if (inited)
            {
                return;
            }

#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS
            ///
            if (!Advertisement.isInitialized)
            {
                Advertisement.Initialize(CurrentGameId);
            }

            ///
            RemoteSettings_Updated();
            RemoteSettings.Updated += RemoteSettings_Updated;

            ///
            UnityThreadHelper.Instance.StartCoroutine(WaitForUnityAdvertisementsInitialized());
#endif

            ///
            inited = true;
        }

        static IEnumerator WaitForUnityAdvertisementsInitialized()
        {
            ///
            while (!Advertisement.isInitialized)
            {
                yield return null;
            }

            ///
            OnUnityAdvertisementsInitialized?.Invoke();
        }

        static void RemoteSettings_Updated()
        {
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS
            UseUnityAds = RemoteSettings.GetBool(RemoteKeys.UseUnityAds, true);
#endif
        }
    }
}
#endif