using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    [System.Serializable]
    public struct RandomInt
    {
        [SerializeField]
        int minValue;
        [SerializeField]
        int maxValue;

        int pinnedValue;

        public int PinnedValue
        {
            get
            {
                return pinnedValue;
            }

            private set
            {
                pinnedValue = value;
            }
        }

        public void PinAValue()
        {
            PinnedValue = this;
        }

        public int MaxValue
        {
            get
            {
                return maxValue;
            }

            private set
            {
                maxValue = value;
            }
        }

        public int MinValue
        {
            get
            {
                return minValue;
            }

            private set
            {
                minValue = value;
            }
        }

        public RandomInt(int min, int max)
        {
            minValue = min;
            maxValue = max;
            pinnedValue = min;
        }

        public static implicit operator int(RandomInt randomFloat)
        {
            return Random.Range(randomFloat.MinValue, randomFloat.MaxValue);
        }
    }

}