using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FH.Core.HelperComponent
{
    public class UrlLauncher : MonoBehaviour
    {
        [SerializeField]
        string url = "";

        public void Launch()
        {
            Application.OpenURL(url);
        }
    }

}