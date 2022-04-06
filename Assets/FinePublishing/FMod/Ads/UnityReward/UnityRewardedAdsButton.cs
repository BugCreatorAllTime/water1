using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FMod
{
    public class UnityRewardedAdsButton : MonoBehaviour
    {
        [SerializeField]
        bool monetizableAdsOnly = false;

        [Space]
        [SerializeField]
        UnityEvent setAvailableState = new UnityEvent();
        [SerializeField]
        UnityEvent setUnavailableState = new UnityEvent();

        bool lastState;

        private void OnEnable()
        {
            ///
            lastState = RewardedAds.IsAvailable;

            ///
            UpdateButtonState(lastState);
        }

        private void Update()
        {
            ///
            var currentState = monetizableAdsOnly ? RewardedAds.IsMonetizableAdsAvailable : RewardedAds.IsAvailable;
            if (currentState != lastState)
            {
                ///
                lastState = currentState;

                ///
                UpdateButtonState(lastState);
            }
        }

        void UpdateButtonState(bool isAvailable)
        {
            if (isAvailable)
            {
                setAvailableState?.Invoke();
            }
            else
            {
                setUnavailableState?.Invoke();
            }
        }
    }

}