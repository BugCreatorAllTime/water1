// #define ENABLE_FIREBASE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMod;
using System;
using Firebase.Analytics;

namespace FMod
{
    public class MyAnalytics : MonoBehaviour
    {
        #region Facebook
        public static void Facebook_RewardShown()
        {
            Analytics.LogEvent("rewarded_shown", 1);
        }

        public static void Facebook_Complete_Level()
        {
            Analytics.LogEvent("complete_level", 1);
        }
        #endregion

        #region Firebase
        public static void FirebaseNoAdsBaseUIClick()
        {
            LogFirebaseEvent("no_ads_base_ui");
        }

        public static void FirebaseNoAdsInShopClick()
        {
            LogFirebaseEvent("no_ads_inshop");
        }


        public static void Firebase_ShowInter()
        {
            LogFirebaseEvent("inter_show");
        }

        public static void Firebase_ShowReward()
        {
            LogFirebaseEvent("reward_show");
        }

        public static void Firebase_CompletedReward()
        {
            LogFirebaseEvent("reward_completed");
        }

        public static void Firebase_ShowRewardType(string pType)
        {
            LogFirebaseEvent("reward_show_", "reward_type", pType);
        }

        public static void Firebase_CompletedRewardType(string pType)
        {
            LogFirebaseEvent("reward_completed_", "reward_type", pType);
        }

        public static void Firebase_Undo()
        {
            LogFirebaseEvent("undo_step");
        }

        public static void Firebase_ResetLevel(int level)
        {
            int levelGroup = level / 20;
            string eventName = string.Format("w_reset_lvl_{0}_{1}", levelGroup * 20 + 1, levelGroup * 20 + 20);
            LogFirebaseEvent(eventName, "level_id", level + 1);
        }

        public static void Firebase_Start_Level(int level)
        {
            LogFirebaseEvent("start_level", "num", level);

            int levelGroup = level / 20;
            string eventName = string.Format("w_start_lvl_{0}_{1}", levelGroup * 20 + 1, levelGroup*20 + 20);
            LogFirebaseEvent(eventName, "level_id", level + 1);
        }

        public static void Firebase_Complete_Level(int level)
        {
//#if ENABLE_FIREBASE
            LogFirebaseEvent("complete_level", "num", level);

            int levelGroup = level / 20;
            string eventName = string.Format("complete_level_{0}_{1}", levelGroup * 20 + 1, levelGroup * 20 + 20);
            LogFirebaseEvent(eventName, "level_id", level + 1);
            //#endif
        }

        public static void Firebase_Complete_Level_5x()
        {
//#if ENABLE_FIREBASE
            LogFirebaseEvent("complete_level_5x");
//#endif
        }

        public static void Firebase_Complete_Level_10x()
        {
//#if ENABLE_FIREBASE
            LogFirebaseEvent("complete_level_10x"); 
//#endif
        }

        public static void Firebase_Complete_Level_15x()
        {
//#if ENABLE_FIREBASE
            LogFirebaseEvent("complete_level_15x"); 
//#endif
        }

        public static void Firebase_Complete_Level_20x()
        {
//#if ENABLE_FIREBASE
            LogFirebaseEvent("complete_level_20x"); 
//#endif
        }

        public static void Firebase_Theme_Unlock(int themeId)
        {
//#if ENABLE_FIREBASE
            LogFirebaseEvent("theme_unlock", "name", themeId + 1); 
//#endif
        }

        public static void Firebase_Bottle_Unlock(int tubeId)
        {
//#if ENABLE_FIREBASE
            LogFirebaseEvent("bottle_unlock", "name", tubeId + 1); 
//#endif
        }

        static void LogFirebaseEvent(string eventName)
        {
            try
            {
                FirebaseAnalytics.LogEvent(eventName);
            }
            catch (Exception ex)
            {
                Debug.LogError("Cannot log firebase: event " + eventName + " by " + ex.Message);
            }
        }

        static void LogFirebaseEvent(string eventName, string para, string value)
        {
            try
            {
                FirebaseAnalytics.LogEvent(eventName, para, value);
            }
            catch (Exception ex)
            {
                Debug.LogError("Cannot log firebase: event " + eventName + " para " + para + " value " + value + " by " + ex.Message);
            }
        }

        static void LogFirebaseEvent(string eventName, string para, int value)
        {
            try
            {
                FirebaseAnalytics.LogEvent(eventName, para, value);
            }
            catch (Exception ex)
            {
                Debug.LogError("Cannot log firebase: event " + eventName + " para " + para + " value " + value + " by " + ex.Message);
            }
        }
        #endregion
    }
}
