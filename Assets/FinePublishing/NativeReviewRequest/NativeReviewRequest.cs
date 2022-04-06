/*
 * Made by Kamen Dimitrov, http://www.studio-generative.com
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class NativeReviewRequest
{
    public static bool disableNativeRequest = false;

    public static bool RequestReview()
    {
        if (disableNativeRequest)
        {
            return false;
        }

#if UNITY_IOS && !UNITY_EDITOR
		return requestReview();
#endif

        return false;
    }

#if UNITY_IOS && !UNITY_EDITOR
	[DllImport ("__Internal")] private static extern bool requestReview();
#endif
}
