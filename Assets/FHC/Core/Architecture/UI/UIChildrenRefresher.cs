using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Architecture.UI
{
    [DisallowMultipleComponent]
    public class UIChildrenRefresher : MonoBehaviour, IUIRefresher
    {
        [ContextMenu("Refresh")]
        public void Refresh()
        {
            foreach (Transform item in transform)
            {
                IUIRefresher refresher = item.GetComponent<IUIRefresher>();
                if (refresher != null)
                {
                    refresher.Refresh();
                }
            }
        }
    }

}