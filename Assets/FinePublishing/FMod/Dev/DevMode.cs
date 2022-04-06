using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public class DevMode : MonoBehaviour
    {
        static bool isInDevMode = false;

        public static bool IsInDevMode
        {
            get
            {
                return isInDevMode;
            }
        }

        public static void SetAsDevMode()
        {
            isInDevMode = true;
        }
    }

}