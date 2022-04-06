//#define USE_FIREBASE_ANALYTICS
//#define USE_UNITY_ANALYTICS
//#define USE_FACEBOOK_IAP;

#if !UNITY_STANDALONE
//#define USE_TENJIN  
//#define USE_GETSOCIAL
//#define USE_APPODEAL
#endif


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
#if USE_APPODEAL
using AppodealAds.Unity.Api;
#endif
#if UNITY_IOS && USE_FIREBASE_ANALYTICS
using Firebase.Analytics;
#endif

#if UNITY_STANDALONE
//using PlayFab;
//using PlayFab.ClientModels;
#endif

namespace FMod
{
    public static class Analytics
    {
        struct AnalyticsData
        {
            public string eventName;
            public float numberToSum;
            public Dictionary<string, object> parameters;
        };

        static List<AnalyticsData> cachedFacebookAnalytics = new List<AnalyticsData>();

        internal static void FlushFacebookCache()
        {
            foreach (var item in cachedFacebookAnalytics)
            {
                LogWithFacebook(item.eventName, item.numberToSum, item.parameters);
            }

            cachedFacebookAnalytics.Clear();
        }

        public static void LogEvent(string eventName, string parameterName, object parameterValue, float numberToSum = 1)
        {
            ///
            var parameters = new Dictionary<string, object>()
            {
                { parameterName, parameterValue}
            };

            ///
            LogEvent(eventName, numberToSum, parameters);
        }

        public static void LogEvent(string eventName, float numberToSum, Dictionary<string, object> parameters = null)
        {
#if !UNITY_STANDALONE
            LogWithFirebase(eventName, numberToSum, parameters);
            TryLogWithFacebook(eventName, numberToSum, parameters);
            LogWithUnity(eventName, numberToSum, parameters);
            LogConsole(eventName, numberToSum, parameters);
#endif

#if UNITY_STANDALONE
            LogWithPlayFab(eventName);
#endif
        }

        public static void SetScreen(string screenName, string screenClass)
        {
#if UNITY_IOS && USE_FIREBASE_ANALYTICS
            ///
            if (!FirebaseAnalyticsInit.Inited)
            {
                return;
            }

            ///
            FirebaseAnalytics.SetCurrentScreen(screenName, screenClass);
#endif
        }

        public static void ClearScreen(string screenClass)
        {
#if UNITY_IOS && USE_FIREBASE_ANALYTICS
            ///
            if (!FirebaseAnalyticsInit.Inited)
            {
                return;
            }

            ///
            FirebaseAnalytics.SetCurrentScreen(null, screenClass);
#endif
        }

        static void LogConsole(string eventName, float numberToSum, Dictionary<string, object> parameters = null, bool cached = false)
        {
#if UNITY_EDITOR
            string paramsLog = "";
            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    paramsLog += '\n' + string.Format("{0} - {1}", item.Key, item.Value);
                }
            }
            else
            {
                paramsLog = "<NULL>";
            }
            Debug.Log(string.Format("FMod Analytics{0}: {1} - {2} - {3}", cached ? "(Cache):" : "", eventName, numberToSum, paramsLog));
#endif
        }

        static void TryLogWithFacebook(string eventName, float numberToSum, Dictionary<string, object> parameters = null)
        {
            if (FB.IsInitialized)
            {
                LogWithFacebook(eventName, numberToSum, parameters);
            }
            else
            {
                cachedFacebookAnalytics.Add(new FMod.Analytics.AnalyticsData()
                {
                    eventName = eventName,
                    numberToSum = numberToSum,
                    parameters = parameters
                });
            }
        }

        static void LogWithFacebook(string eventName, float numberToSum, Dictionary<string, object> parameters = null)
        {
            FB.LogAppEvent(eventName, numberToSum, parameters);
        }

#if UNITY_STANDALONE
        static void LogWithPlayFab(string eventName)
        {
//#if !UNITY_EDITOR
//            ///
//            if (!PlayFabClientAPI.IsClientLoggedIn())
//            {
//                return;
//            }

//            ///
//            WriteClientPlayerEventRequest rq = new WriteClientPlayerEventRequest()
//            { EventName = eventName };
//            PlayFabClientAPI.WritePlayerEvent(rq, null, null); 
//#endif
        }
#endif

        static void LogWithFirebase(string eventName, float numberToSum, Dictionary<string, object> parameters = null)
        {
#if UNITY_IOS && USE_FIREBASE_ANALYTICS 
            ///
            if (!FirebaseAnalyticsInit.Inited)
            {
                return;
            }

            ///
            Firebase.Analytics.FirebaseAnalytics.LogEvent(eventName); 
#endif
        }

        static void LogWithUnity(string eventName, float numberToSum, Dictionary<string, object> parameters = null)
        {
#if USE_UNITY_ANALYTICS

            if (Application.isEditor)
            {
                UnityEngine.Analytics.Analytics.CustomEvent("TestCustomEvent");
                UnityEngine.Analytics.Analytics.FlushEvents();
                return;
            }

            ///
            var rs = UnityEngine.Analytics.Analytics.CustomEvent(eventName);
            return;
#endif
        }

        #region Standard Events
        public static void LogSE_CompleteTutorial(bool isSuccess)
        {
            LogFacebookSE_CompleteTutorial(isSuccess);
            LogUnitySE_CompleteTutorial(isSuccess);
            LogTenjinSE_CompleteTutorial();
        }

        public static void LogSE_CompleteAchievement(string description)
        {
            LogFacebookSE_CompleteAchievement(description);
            LogUnitySE_CompleteAchievement(description);
            LogTenjinSE_CompleteAchievement();
        }

        public static void LogSE_AchievedLevel(int level)
        {
            ///
            var levelStr = level.ToString();

            ///
            LogFacebookSE_AchievedLevel(levelStr);
            LogUnitySE_AchievedLevel(levelStr);
            LogGetSocialSE_AchievedLevel(levelStr);
            LogTenjinSE_AchievedLevel(levelStr);
        }

        public static void LogSE_Rating(int ratedValue, int maxRating)
        {
            LogFacebookSE_Rating(ratedValue, maxRating);
        }

        public static void LogSE_Purchase(float priceAmount, string priceCurrency, string itemId)
        {
#if USE_FACEBOOK_IAP
            LogFacebookSE_Purchase(priceAmount, priceCurrency, itemId, product, retentionDays);
#endif
#if !UNITY_EDITOR
            LogTenjinSE_Purchase(priceAmount, priceCurrency, itemId, product); 
#endif
#if USE_APPODEAL
            Appodeal.trackInAppPurchase(priceAmount, priceCurrency);
#endif
        }
        #endregion

        #region Facebook's standard events

        /*static void LogFacebookSE_Purchase(float priceAmount, string priceCurrency, string itemId)
        {
            ///
            var iapParameters = new Dictionary<string, object>();
            iapParameters["itemId"] = itemId;
            iapParameters["transactionId"] = product.transactionID;
            iapParameters["unityTime"] = Time.time;
            iapParameters["retentionDays"] = retentionDays;
#if UNITY_IOS
            if (UnityEngine.iOS.Device.advertisingTrackingEnabled)
            {
                iapParameters["useId"] = UnityEngine.iOS.Device.advertisingIdentifier;
            }
#endif

            ///
            FB.LogPurchase(priceAmount, priceCurrency, iapParameters);
        }*/

        static void LogFacebookSE_CompleteTutorial(bool isSuccess)
        {
            ///
            string name = AppEventName.CompletedTutorial;
            float valueToSome = 1;
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {AppEventParameterName.Success, isSuccess?1:0}
            };

            ///
            LogEvent(name, valueToSome, parameters);
        }

        static void LogFacebookSE_CompleteAchievement(string description)
        {
            ///
            string name = AppEventName.UnlockedAchievement;
            float valueToSome = 1;
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {AppEventParameterName.Description, description}
            };

            ///
            LogEvent(name, valueToSome, parameters);
        }

        static void LogFacebookSE_AchievedLevel(string level)
        {
            ///
            string name = AppEventName.AchievedLevel;
            float valueToSome = 1;
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {AppEventParameterName.Level, level},
            };

            ///
            LogEvent(name, valueToSome, parameters);
        }

        static void LogFacebookSE_Rating(int ratedValue, int maxRating)
        {
            ///
            string name = AppEventName.Rated;
            float valueToSome = ratedValue;
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {AppEventParameterName.MaxRatingValue, maxRating}
            };

            ///
            LogEvent(name, valueToSome, parameters);
        }
        #endregion

        #region Unity's standard events
        static void LogUnitySE_CompleteTutorial(bool isSuccess)
        {
#if USE_UNITY_ANALYTICS
            UnityEngine.Analytics.AnalyticsEvent.TutorialComplete(); 
#endif
        }

        static void LogUnitySE_CompleteAchievement(string description)
        {
#if USE_UNITY_ANALYTICS
            UnityEngine.Analytics.AnalyticsEvent.AchievementUnlocked(description); 
#endif
        }

        static void LogUnitySE_AchievedLevel(string level)
        {
#if USE_UNITY_ANALYTICS
            UnityEngine.Analytics.AnalyticsEvent.LevelComplete(level); 
#endif
        }
        #endregion

        #region Tenjin's standard events
        static void LogTenjinSE_Purchase(float priceAmount, string priceCurrency, string itemId)
        {
#if USE_TENJIN
            InitTenjin.LogPurchase(product);
#endif
        }

        static void LogTenjinSE_CompleteTutorial()
        {
#if USE_TENJIN
            InitTenjin.LogCompletedTutorial();
#endif
        }

        static void LogTenjinSE_AchievedLevel(string level)
        {
#if USE_TENJIN
            InitTenjin.LogAchievedLevel(level);
#endif
        }

        static void LogTenjinSE_CompleteAchievement()
        {
#if USE_TENJIN
            InitTenjin.LogEngaged();
#endif
        }
        #endregion

        #region GetSocial's standard events
        public static void LogGetSocialSE_AchievedLevel(string level)
        {
#if USE_GETSOCIAL
            var properties = new Dictionary<string, string>
            {
                {"level", level}
            };

            GetSocialSdk.Core.GetSocial.TrackCustomEvent("level_achieved", properties);
#endif
        }
        #endregion
    }

}