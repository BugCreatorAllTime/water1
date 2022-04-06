using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FH.Core.Architecture;

namespace FMod
{
    public class LevelUnlockedState : MonoBehaviour
    {
        const string StoreKeyTemplate = "LevelUnlockedState_{0}";

        [SerializeField]
        int levelId;

        [SerializeField]
        OrderedEventDispatcher onProceedSucceeded = new OrderedEventDispatcher();
        [SerializeField]
        OrderedEventDispatcher onProceedFailed = new OrderedEventDispatcher();

        public static string GetLevelStoreKey(int levelId)
        {
            return string.Format(StoreKeyTemplate, levelId);
        }

        public static bool IsLevelUnlocked(int levelId)
        {
            return PlayerPrefs.GetInt(GetLevelStoreKey(levelId), 0) == 1;
        }

        public static void UnlockLevel(int levelId)
        {
            PlayerPrefs.SetInt(GetLevelStoreKey(levelId), 1);
            PlayerPrefs.Save();
        }

        public bool IsLevelUnlocked()
        {
            return IsLevelUnlocked(levelId);
        }

        public void ProceedToLevel()
        {
            if (IsLevelUnlocked())
            {
                onProceedSucceeded.Dispatch();
            }
            else
            {
                onProceedFailed.Dispatch();
            }
        }

        public void UnlockLevel()
        {
            UnlockLevel(levelId);
        }
    }

}