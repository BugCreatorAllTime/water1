using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FH.Core.Architecture;

namespace FH.Core.Gameplay.HelperComponent
{
    public class ManualCounterTriggerer : MonoBehaviour
    {
        [SerializeField]
        int maxCount = 3;
        [SerializeField]
        Clamping clamping = Clamping.Once;

        [Space]
        [SerializeField]
        OrderedEventDispatcher onMaxed;

        int currentCount;

        public enum Clamping
        {
            Loop,
            Once,
            Infinite
        }

        public void ResetCount()
        {
            currentCount = 0;
        }

        public void IncreaseCount()
        {
            ///
            currentCount++;

            ///
            if (currentCount >= maxCount)
            {
                switch (clamping)
                {
                    case Clamping.Loop:
                        onMaxed?.Dispatch();
                        currentCount = 0;
                        break;
                    case Clamping.Once:
                        if (currentCount == maxCount)
                        {
                            onMaxed?.Dispatch();
                        }
                        else
                        {
                            currentCount = maxCount + 1;
                        }
                        break;
                    case Clamping.Infinite:
                        onMaxed?.Dispatch();
                        currentCount = maxCount + 1;
                        break;
                    default:
                        throw new System.NotImplementedException();
                }
            }
        }
    }

}
