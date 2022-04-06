
using System.Collections;
using System.Collections.Generic;
using FMod;
using UnityEngine;

public class AddCoinsButton : MonoBehaviour
{
    
    
    [SerializeField]
    private int coinsAmount = 100;
    [SerializeField]
    private float countdownSeconds = 10 * 60;
    [SerializeField]
    UnityEngine.UI.Button button;
    [SerializeField]
    private GameObject countdownObject;
    [SerializeField]
    private UnifiedText countdownText;

    private static System.DateTime lastTimeWatched = System.DateTime.MinValue;
    
    private bool isCountingDown;
    
    public void OnEnable()
    {
        UpdateView();
    }

    public void Update()
    {
        if (isCountingDown)
        {
            UpdateCountdown();
        }
    }

    public void HandlTap()
    {
        ///
        if (isCountingDown)
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
            MyAnalytics.Firebase_ShowRewardType("Shop_MoreCoin");
            if (success)
            {
                OnGotReward();
                
                UpdateView();
                MyAnalytics.Firebase_CompletedRewardType("Shop_MoreCoin");
            }
        });
    }

    public void OnGotReward()
    {
        // Add coins
        Entry.Instance.playerDataObject.Data.AddCoins(coinsAmount);

        ///
        lastTimeWatched = System.DateTime.Now;
        
        
    }

    private void UpdateCountdown()
    {
        var timePassed = System.DateTime.Now - lastTimeWatched;
        

        if (timePassed.TotalSeconds < countdownSeconds)
        {
            
            var displayTime = System.TimeSpan.FromSeconds(countdownSeconds - timePassed.TotalSeconds);
            countdownText.Text = string.Format("{0}:{1:00}", displayTime.Minutes, displayTime.Seconds);
        }
        else
        {
            UpdateView();
        }
    }

    private void UpdateView()
    {
        var timePassed = System.DateTime.Now - lastTimeWatched;
        if (timePassed.TotalSeconds < countdownSeconds)
        {
            button.interactable = false;
            countdownObject.SetActive(true);
            isCountingDown = true;
        }
        else
        {
            button.interactable = true;
            countdownObject.SetActive(false);
            isCountingDown = false;
        }
    }
}
