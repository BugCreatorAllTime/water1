using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace FMod
{
    public class UrlLauncher : MonoBehaviour
    {
        [SerializeField]
        string analyticsEventTag = "UntaggedUrl";
        [SerializeField]
        MString firstUrl;
        [SerializeField]
        MString secondUrl;
        [SerializeField]
        float delayTime = 1.0f;
        [SerializeField]
        float delayFrames = 10;

        bool launching = false;

#if UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void OpenUrlInNewWindow(string url);
#endif

        public void Launch()
        {
            if (launching)
            {
                return;
            }

#if UNITY_WEBGL && !UNITY_EDITOR
            OpenUrlInNewWindow(firstUrl);
            return;
#endif

            StartCoroutine(LaunchAsync());

            Analytics.LogEvent("LaunchUrl", "Tag", analyticsEventTag, 1);
        }

        IEnumerator LaunchAsync()
        {
            launching = true;

            float startTime = Time.realtimeSinceStartup;
            int framesCount = 0;

            Application.OpenURL(firstUrl);

            while (framesCount < delayFrames)
            {
                framesCount++;
                yield return null;
            }

            if ((Time.realtimeSinceStartup - startTime) <= delayTime)
            {
                Application.OpenURL(secondUrl);
            }

            launching = false;
        }
    }

}