using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public delegate void RewardedAdsClosedCallback(bool isSuccessul);

    public interface IRewardedAds : IAds
    {
        bool IsAvailable { get; }
        /// <summary>
        /// Can we earn real cash directly from impressions?
        /// </summary>
        bool IsMonetizable { get; }
        void Show(RewardedAdsClosedCallback onClosedCallback);
    }

}