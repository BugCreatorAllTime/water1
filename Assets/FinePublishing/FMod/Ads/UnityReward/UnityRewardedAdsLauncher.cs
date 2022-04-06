using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FH.Core.Architecture;

namespace FMod
{
    public class UnityRewardedAdsLauncher : MonoBehaviour
    {
        [SerializeField]
        OrderedEventDispatcher onCompleted = new OrderedEventDispatcher();
        [SerializeField]
        OrderedEventDispatcher onSkipped = new OrderedEventDispatcher();

        public void TryShowAds()
        {
            if (FMod.RewardedAds.IsAvailable)
            {
                FMod.RewardedAds.Show(
                         (bool rs) =>
                          {
                              if (rs)
                              {
                                  onCompleted.Dispatch();
                              }
                              else
                              {
                                  onSkipped.Dispatch();
                              }
                          }
                    );
            }
        }
    }

}