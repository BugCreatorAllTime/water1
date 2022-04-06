using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public enum BannerAdsPosition
    {
        Top,
        Bottom
    }

    public interface IBannerAds : IAds
    {
        event BannerAvailabilityChangedHandler OnAvailabilityChanged;
        int ListId { get; set; }
        bool IsAvailable { get; }
        void Show(BannerAdsPosition position);
        void Hide();
    }

    public delegate void BannerAvailabilityChangedHandler(IBannerAds bannerAds);
}