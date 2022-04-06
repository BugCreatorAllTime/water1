using UnityEngine;
using UnityEngine.Events;

namespace FMod
{
    public class AdmobInterstitialController : MonoBehaviour
    {
        public static bool EnableAds
        {
            get
            {
                return Ads.EnableInterAndBanner;
            }

            set
            {
                Ads.EnableInterAndBanner = value;
                if (!value && OnAdsDisabled != null)
                {
                    OnAdsDisabled();
                }
            }
        }

        public static event System.Action OnAdsDisabled;

        [Space]
        [SerializeField]
        UnityEvent onFailedToShowAds = new UnityEvent();
        [SerializeField]
        UnityEvent onShowAds = new UnityEvent();
        [SerializeField]
        UnityEvent onClosedAds = new UnityEvent();


        public void TryShowAds()
        {
            if (InterstitialAds.IsAvailable)
            {
                onShowAds.Invoke();
                InterstitialAds.Show(() => { onClosedAds.Invoke(); });
            }
            else
            {
                onFailedToShowAds.Invoke();
            }
        }

        public void TryShowAdsDelay(float delayTime)
        {
            Invoke("TryShowAds", delayTime);
        } 

    }

}