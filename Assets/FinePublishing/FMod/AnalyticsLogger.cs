using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public class AnalyticsLogger : MonoBehaviour
    {
        [SerializeField]
        string eventName;
        [SerializeField]
        float valueToSum = 1;

        public void LogEvent()
        {
            Analytics.LogEvent(eventName, valueToSum);
        }
    }

}