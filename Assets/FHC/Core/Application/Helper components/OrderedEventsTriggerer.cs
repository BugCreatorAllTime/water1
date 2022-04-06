using UnityEngine;
using FH.Core.Architecture;

namespace FH.Core.HelperComponent
{
    public class OrderedEventsTriggerer : MonoBehaviour
    {
        [SerializeField]
        OrderedEventDispatcher events = new OrderedEventDispatcher();

        [ContextMenu("Trigger")]
        public void Trigger()
        {
            events.Dispatch();
        }
    }

}