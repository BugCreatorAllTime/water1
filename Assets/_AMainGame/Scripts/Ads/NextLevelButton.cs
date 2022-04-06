using System.Collections;
using System.Collections.Generic;
using FMod;
using UnityEngine;
using UnityEngine.Events;

public class NextLevelButton : MonoBehaviour
{
    private const int bonusCoin = 200;

    [SerializeField]
    private GameObject watchVideoIcon;

    [Space]
    [SerializeField]
    private int minLevel = 12;
    [SerializeField]
    private int levelCount = 6;

    [Space]
    [SerializeField]
    private UnityEvent onNextLevel;

    private int lastWatchedLevel = -1;

    public void Awake()
    {
        int currentLevel = Entry.Instance.playerDataObject.Data.MaxLevelReached;
        lastWatchedLevel = Mathf.Max(currentLevel, minLevel);
    }

    public void OnEnable()
    {
        ///
        bool shouldWatchAds = ShouldWatchAds();

        ///
        watchVideoIcon.SetActive(shouldWatchAds);
    }

    public void NextLevel(bool useX3)
    {
        if (useX3)
        {
            ///
            if (Ads_Controller.Is_RewardedVideoAvailable)
            {
                Ads_Controller.Play_RewardedVideoAds((success) =>
                {
                    MyAnalytics.Firebase_ShowRewardType("NextLevel");
                    if (success)
                    {
                        Entry.Instance.playerDataObject.Data.AddCoins(bonusCoin * 3);

                        ///
                        lastWatchedLevel = Entry.Instance.playerDataObject.Data.MaxLevelReached;
                        NextLevelImmediately();

                        MyAnalytics.Firebase_CompletedRewardType("NextLevel");
                    }
                });
            }
        }
        else
        {
            Entry.Instance.playerDataObject.Data.AddCoins(bonusCoin);
            NextLevelImmediately();
        }
    }

    private void NextLevelImmediately()
    {
        ///
        Entry.Instance.gameStateManager.SwitchToBeat();

        ///
        onNextLevel?.Invoke();
    }

    private bool ShouldWatchAds()
    {
        ///
        if (!FMod.Ads.EnableInterAndBanner)
        {
            return false;
        }

        ///
        int currentLevel = Entry.Instance.playerDataObject.Data.MaxLevelReached;

        ///
        if (currentLevel < minLevel)
        {
            return false;
        }

        ///
        return (currentLevel - lastWatchedLevel) >= levelCount;
    }
}
