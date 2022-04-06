using System.Collections;
using System.Collections.Generic;
using FMod;
using UnityEngine;
using UnityEngine.UI;

public class RemoveAdsButton : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private bool hideGameObject;

    [SerializeField] bool isBaseUI = false;

    public void Awake()
    {
        FMod.Ads.OnRemovedAds += Ads_OnRemovedAds;
        button.onClick.AddListener(OnRemoveAdsClick);
    }

    private void Ads_OnRemovedAds()
    {
        UpdateView();
    }

    public void OnEnable()
    {
        UpdateView();
    }

    void OnRemoveAdsClick()
    {
        if (isBaseUI)
        {
            MyAnalytics.FirebaseNoAdsBaseUIClick();
        }
        else
        {
            MyAnalytics.FirebaseNoAdsInShopClick();
        }
    }

    private void UpdateView()
    {
        if (!hideGameObject)
        {
            button.interactable = FMod.Ads.EnableInterAndBanner;
        }
        else
        {
            gameObject.SetActive(FMod.Ads.EnableInterAndBanner);
        }
    }
}
