using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsSwitcher : MonoBehaviour
{
    [SerializeField]
    Text buttonText;

    public void OnEnable()
    {
        UpdateText();
    }

    public void Switch()
    {
        FMod.Ads.TestInterAndBanner = !FMod.Ads.TestInterAndBanner;
        UpdateText();
    }

    void UpdateText()
    {
        buttonText.text = string.Format("Ads: {0}", FMod.Ads.TestInterAndBanner);
    }
}
