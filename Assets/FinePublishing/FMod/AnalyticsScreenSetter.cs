using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public class AnalyticsScreenSetter : MonoBehaviour
    {
        [SerializeField]
        string screenName;
        [SerializeField]
        string screenClass = "AppRoot";
        [SerializeField]
        bool clearScreenOnDisable = false;

        public void OnEnable()
        {
            Analytics.SetScreen(screenName, screenClass);
        }

        public void OnDisable()
        {
            Analytics.ClearScreen(screenClass);
        }
    }

}