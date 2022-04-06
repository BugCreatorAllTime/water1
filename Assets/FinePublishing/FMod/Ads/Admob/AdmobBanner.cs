using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if USE_STANDALONE_ADMOB
//using GoogleMobileAds.Api; 
#endif
using System;

namespace FMod
{
    public class AdmobBanner : AdmobBannerBase, IBannerAds
    {
        [SerializeField]
        MString adUnitId = new MString();

        public string AdId => AdsIds.AdmobBanner;

        public bool IsAvailable => throw new NotImplementedException();

        public int ListId { get; set; }

        public event BannerAvailabilityChangedHandler OnAvailabilityChanged;

//#if USE_STANDALONE_ADMOB
        //public void Awake()
        //{
        //    UnityEngine.Assertions.Assert.IsTrue(adUnitId.VerifyEncryption());
        //}

        //private void OnEnable()
        //{
        //    Show();
        //}

        //public void OnDisable()
        //{
        //    Hide();
        //}

        //public void Show(BannerAdsPosition position)
        //{
        //    ///
        //    enabled = true;
        //}

        //public new void Hide()
        //{
        //    ///
        //    enabled = false;

        //    ///
        //    base.Hide();
        //}

        //protected override string GetBannerUnitId()
        //{
        //    return adUnitId;
        //}

        //[ContextMenu("TryEncryptAdUnit")]
        //void TryEncryptAdUnit()
        //{
        //    adUnitId.TryFillEncryptionFields();
        //}

        //[ContextMenu("VerifyEncryption")]
        //void VerifyEncryption()
        //{
        //    Debug.Log(adUnitId.VerifyEncryption());
        //} 
//#else
        public void Hide()
        {
            throw new NotImplementedException();
        }

        public void Show(BannerAdsPosition position)
        {
            throw new NotImplementedException();
        }
//#endif
    }

}