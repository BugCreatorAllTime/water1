using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public class FacebookInit : MonoBehaviour
    {
#if !UNITY_STANDALONE
        public void Awake()
        {
            if (!Facebook.Unity.FB.IsInitialized)
            {
                Facebook.Unity.FB.Init(OnFBInitialized);
            }
        }


        void OnFBInitialized()
        {
            Analytics.FlushFacebookCache();
        } 
#endif
    }

}