using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public class DevModeMarker : MonoBehaviour
    {
        public void Awake()
        {
            DevMode.SetAsDevMode();
        }
    }

}