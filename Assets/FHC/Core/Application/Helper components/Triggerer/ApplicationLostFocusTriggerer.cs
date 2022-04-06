using UnityEngine;
using System.Collections;
using FH.Core.Architecture;

namespace FH.Core.HelperComponent
{
    public class ApplicationLostFocusTriggerer : MonoBehaviour
    {
        [SerializeField]
        bool ignoreInEditor = false;

        [SerializeField]
        OrderedEventDispatcher onLost = new OrderedEventDispatcher();

        [SerializeField]
        OrderedEventDispatcher onGot = new OrderedEventDispatcher();

        public void OnApplicationFocus(bool focus)
        {
#if UNITY_EDITOR
            if (ignoreInEditor)
            {
                return;
            }
#endif

            if (!focus)
            {
                FHLog.Log("App lost focus", this);
                onLost.Dispatch();
            }
            else
            {
                FHLog.Log("App got focus", this);
                onGot.Dispatch();
            }
        }
    }

}