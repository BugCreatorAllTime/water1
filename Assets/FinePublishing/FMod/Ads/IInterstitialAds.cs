using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public interface IInterstitialAds : IAds
    {
        bool IsAvailable { get; }
        void Show(System.Action onClosedCallback);
    }

}