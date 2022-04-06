using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInterAdsOnAppResume : MonoBehaviour
{
    [SerializeField]
    private float minSeconds = 10.0f;

    public void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            ///
            if (Time.unscaledTime < minSeconds)
            {
                return;
            }

            ///
            StartCoroutine(TryShowDelay());
        }
        else
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator TryShowDelay()
    {
        ///
        yield return null;
        yield return null;

        ///
        FMod.InterstitialAds.ShowQuick(null);
    }
}
