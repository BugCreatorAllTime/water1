using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FH.Core.Architecture;

namespace FH.Core.HelperComponent
{
    public class RendererVisibilityTriggerer : MonoBehaviour
    {
        [SerializeField]
        OrderedEventDispatcher onVisible;
        [SerializeField]
        OrderedEventDispatcher onInvisible;

        public void OnBecameInvisible()
        {
            onInvisible.Dispatch();
        }

        public void OnBecameVisible()
        {
            onVisible.Dispatch();
        }

    }

}