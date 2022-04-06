using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase.Analytics;

[System.Serializable]
public partial class PlayerData
{
    //public static PlayerData InstancePlayerData;
    #region EVENTS   
    public static event Action OnLeveledUp;
    public static event Action OnSpentCoin;
    public static event Action OnAddedCoin;
    public static event Action OnCoinsChanged;
    
    public static event Action OnTubeSkinChanged;
    public static event Action<int> OnSkinUnlocked;
    public static event Action OnThemeChanged;
    public static event Action<int> OnThemeUnlocked;
    public static event Action OnLevelDifficultyChanged;
    #endregion

    #region FIELDS

    [Header("Level")]
    [SerializeField]
    private int level;
    [SerializeField]
    private int maxLevelReached = -1;
    [SerializeField]
    private int levelDifficultyChangeCount;
    [SerializeField]
    private List<bool> levelsHardModeCompleted = new List<bool>();

    [Header("Currency")]
    [SerializeField]
    private double coins;

    [Header("TubeSkin")]
    [SerializeField]
    private int currentTubeSkinId = 0;
    [SerializeField]
    private List<bool> tubeSkinStates;

    [Header("Theme")]
    [SerializeField]
    private int currentThemeId = 0;
    [SerializeField]
    private List<bool> themeStates;

    [Header("Level generation")]
    public int lastLoadedlevel;

    [Header("Other")]
    public int availableUndoCount;

    [Header("Analytics data")]
    [SerializeField]
    private int gameCount;
    [SerializeField]
    private int gameCountThisLevel;
    [SerializeField]
    private int gameCountLastLevel;

    [Header("Time log")]
    [SerializeField]
    private long lastTimeSaved = -99;
    [SerializeField]
    private long installTime = -1;
    [SerializeField]
    private float timeSpentInGame = 0;

    [Header("Version")]
    [SerializeField]
    private string lastLaunchVersion;
    [SerializeField]
    private string currentVersion;

    #endregion

    #region PROPERTIES    

    public int Level { get => level; }

    public double Coins { get => coins; }

    public long LastTimeSaved
    {
        get
        {
            return lastTimeSaved;
        }

        set
        {
            lastTimeSaved = value;
        }
    }

    /// <summary>
    /// by seconds
    /// </summary>
    public float TimeSpentInGame
    {
        get
        {
            return timeSpentInGame;
        }

        private set
        {
            timeSpentInGame = value;
        }
    }

    public long InstallTime
    {
        get
        {
            return installTime;
        }

        private set
        {
            installTime = value;
        }
    }

    public int GameCount { get => gameCount; private set => gameCount = value; }
    public int GameCountThisLevel { get => gameCountThisLevel; private set => gameCountThisLevel = value; }
    public int GameCountLastLevel { get => gameCountLastLevel; private set => gameCountLastLevel = value; }

    public string LastLaunchVersion { get => lastLaunchVersion; private set => lastLaunchVersion = value; }
    public string CurrentVersion { get => currentVersion; private set => currentVersion = value; }

    public int CurrentTubeSkinId => currentTubeSkinId;
    public int CurrentThemeId => currentThemeId;
    
    public int UnlockedTubeSkinCount
    {
        get
        {
            ///
            if (tubeSkinStates == null)
            {
                return 0;
            }

            ///
            int count = 0;
            foreach (var item in tubeSkinStates)
            {
                if (item)
                {
                    count++;
                }
            }

            ///
            return count;
        }
    }


    public int LevelDifficultyChangeCount { get => levelDifficultyChangeCount; private set => levelDifficultyChangeCount = value; }
    public int MaxLevelReached { get => maxLevelReached; private set => maxLevelReached = value; }
    #endregion

    #region METHODS

    public void UpdateVersions()
    {
        LastLaunchVersion = CurrentVersion;
        CurrentVersion = Application.version;
    }

    #region Analytics
    public void AddTimeSpentInGame(float seconds)
    {
        timeSpentInGame += seconds;
    }

    public bool TrySetNowAsInstallTime()
    {
        if (installTime < 0)
        {
            installTime = System.DateTime.Now.Ticks;
            return true;
        }

        ///
        return false;
    }
    #endregion

    #region TubeSkin
    public bool IsTubeSkinUnlocked(int skinId)
    {
        ///
        if (tubeSkinStates == null)
        {
            return false;
        }

        ///
        if (skinId >= tubeSkinStates.Count)
        {
            return false;
        }

        ///
        return tubeSkinStates[skinId];
    }

    public void UnlockTubeSkin(int skinId)
    {
        ///
        if (tubeSkinStates == null)
        {
            tubeSkinStates = new List<bool>();
        }

        ///
        while (skinId >= tubeSkinStates.Count)
        {
            tubeSkinStates.Add(false);
        }

        ///
        tubeSkinStates[skinId] = true;

        ///
        OnSkinUnlocked?.Invoke(skinId);

        ///
        FMod.MyAnalytics.Firebase_Bottle_Unlock(skinId + 1);
    }

    public void ChangeTubeSkin(int skinId)
    {
        ///
        currentTubeSkinId = skinId;

        ///
        OnTubeSkinChanged?.Invoke();
    }
    #endregion

    #region Theme
    public bool IsThemeUnlocked(int themeId)
    {
        ///
        if (themeStates == null)
        {
            return false;
        }

        ///
        if (themeId >= themeStates.Count)
        {
            return false;
        }

        ///
        return themeStates[themeId];
    }

    public void UnlockTheme(int themeId)
    {
        ///
        if (themeStates == null)
        {
            themeStates = new List<bool>();
        }

        ///
        while (themeId >= themeStates.Count)
        {
            themeStates.Add(false);
        }

        ///
        themeStates[themeId] = true;

        ///
        OnThemeUnlocked?.Invoke(themeId);

        ///
        FMod.MyAnalytics.Firebase_Theme_Unlock(themeId + 1);
    }

    public void ChangeTheme(int themeId)
    {
        ///
        currentThemeId = themeId;

        ///
        OnThemeChanged?.Invoke();
    }
    #endregion
    
    #region Coins
    public bool SpendCoins(double amount)
    {
        ///
        if (amount > coins)
        {
            return false;
        }

        ///
        coins -= amount;

        ///
        OnSpentCoin?.Invoke();
        OnCoinsChanged?.Invoke();

        ///
        return true;
    }

    public void AddCoins(double amount)
    {
        ///
        coins += amount;

        ///
        OnAddedCoin?.Invoke();
        OnCoinsChanged?.Invoke();
    }
    #endregion

    #region Levels
    public void IncreaseGameCount()
    {
        GameCount++;
        GameCountThisLevel++;
    }

    public void LevelUp()
    {
        ///
        level++;

        ///
        MaxLevelReached = Math.Max(MaxLevelReached, level);

        ///
        GameCountLastLevel = GameCountThisLevel;
        GameCountThisLevel = 0;

        ///
        OnLeveledUp?.Invoke();

        ///
        FMod.MyAnalytics.Facebook_Complete_Level();
        FMod.MyAnalytics.Firebase_Complete_Level(level);
        InitTenjin.LogAchievedLevel(level.ToString());

        ///
        switch (level)
        {
            case 5:
                FMod.MyAnalytics.Firebase_Complete_Level_5x();
                break;
            case 10:
                FMod.MyAnalytics.Firebase_Complete_Level_10x();
                break;
            case 15:
                FMod.MyAnalytics.Firebase_Complete_Level_15x();
                break;
            case 20:
                FMod.MyAnalytics.Firebase_Complete_Level_20x();
                break;
            default:
                break;
        }
    }

    public void SetLevelForTest(int testLevel)
    {
        // This is to help old users
        MaxLevelReached = Math.Max(MaxLevelReached, level);

        ///
        if (testLevel < MaxLevelReached)
        {
            level = testLevel;
        }
    }
    public int levelMap;
    public  int Level_InMap;
    public int MaxLevelInMap;
    public void SetCurrentLevel(int levelToSet)
    {
        Level_InMap= Math.Max(maxLevelReached,Level_InMap);
        // This is to help old users
        MaxLevelReached = Math.Max(MaxLevelReached, level);

        ///
        if (levelToSet <= MaxLevelReached)
        {
            Level_InMap=MaxLevelReached;
            level = levelToSet;
            //maxLevelReached=0;
            //maxLevelReached=level;
        }
        levelMap=maxLevelReached;
    }

    public void SetCurrentLevelToMaxLevelReached()
    {
        SetCurrentLevel(level);
    }

    public bool IsCompletedLevelInHardMode(int level)
    {
        ///
        if (levelsHardModeCompleted == null)
        {
            return false;
        }

        ///
        if (level >= levelsHardModeCompleted.Count)
        {
            return false;
        }

        ///
        return levelsHardModeCompleted[level];
    }

    public void SetLevelHardModeCompleted(int level)
    {
        ///
        if (levelsHardModeCompleted == null)
        {
            levelsHardModeCompleted = new List<bool>();
        }

        ///
        while (levelsHardModeCompleted.Count <= level)
        {
            levelsHardModeCompleted.Add(false);
        }

        ///
        levelsHardModeCompleted[level] = true;
    }

    public void SetCurrentLevelHardModeCompleted()
    {
        SetLevelHardModeCompleted(level);
    }

    public int GetNotCompletedHardModeCount()
    {
        int count = 0;

        ///
        for (int i = 0; i < maxLevelReached + 1; i++)
        {
            if (!IsCompletedLevelInHardMode(i))
            {
                count++;
            }
        }

        ///
        return count;
    }
    #endregion

    #region Level generation

    #endregion

    #endregion

#if UNITY_EDITOR

#endif
}
