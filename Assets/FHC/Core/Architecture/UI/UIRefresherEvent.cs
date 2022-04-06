using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Architecture.UI
{
    public class UIRefresherEvent : MonoBehaviour, IUIRefresher
    {
        [SerializeField]
        OrderedEventDispatcher onRefresh = new OrderedEventDispatcher();

        public void Refresh()
        {
            onRefresh.Dispatch();
        }
    }

}