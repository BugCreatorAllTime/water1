using System.Collections;
using System.Collections.Generic;
using FMod;
using UnityEngine;
using UnityEngine.UI;

public class AddTubeButton : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private LevelDrawer levelDrawer;

    [Space]
    [SerializeField]
    private GameObject normalButtonObject;
    [SerializeField]
    private GameObject disabledButtonObject;

    public void Awake()
    {
        Entry.Instance.levelSpawner.OnSpawnedLevelEntry += LevelSpawner_OnSpawnedLevelEntry;

    }

    private void LevelSpawner_OnSpawnedLevelEntry()
    {
        button.interactable = true;
        normalButtonObject.SetActive(true);
        disabledButtonObject.SetActive(false);
    }

    public void HandleClick()
    {
        ///
        if (TubeMovement.IsTransfering)
        {
            return;
        }

        ///
        if (!Ads_Controller.Is_RewardedVideoAvailable && (Application.internetReachability == NetworkReachability.NotReachable))
        {
            Entry.Instance.noInternetPopup.gameObject.SetActive(true);
            return;
        }

        Ads_Controller.Play_RewardedVideoAds((success) =>
        {
            MyAnalytics.Firebase_ShowRewardType("AddTube");
            if (success)
            {
                OnGotReward();
                MyAnalytics.Firebase_CompletedRewardType("AddTube");
            }
        });
    }

    void OnGotReward()
    {
        button.interactable = false;
        normalButtonObject.SetActive(false);
        disabledButtonObject.SetActive(true);
        levelDrawer.AddEmptyTube();
    }
}
