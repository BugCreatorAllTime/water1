using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowIDFA : MonoBehaviour
{
    public void Log()
    {
#if UNITY_IOS
        Debug.LogFormat("iOS IDFA: {0}", UnityEngine.iOS.Device.advertisingIdentifier); 
#elif UNITY_ANDROID
        Application.RequestAdvertisingIdentifierAsync
            (
            (string advertisingId, bool trackingEnabled, string errorMsg) =>
            {
                Debug.LogFormat("Android Advertising ID: {0}", advertisingId);
            }
            );        
#endif
    }
}
