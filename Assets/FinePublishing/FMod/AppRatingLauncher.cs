using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public class AppRatingLauncher : MonoBehaviour
    {
        public void Launch()
        {
            string ratingUrl = AppInfo.Instance.GetRatingUrl();
            if (ratingUrl != null)
            {
                Debug.LogError("Open rating Url: " + ratingUrl);
                Application.OpenURL(ratingUrl);
            }
        }

    }

}