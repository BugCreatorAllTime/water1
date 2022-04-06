using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    [System.Serializable]
    public struct RandomFloat
    {
        [SerializeField]
        float minValue;
        [SerializeField]
        float maxValue;

        float pinnedValue;

        public float PinnedValue
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

        public float MinValue
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

        public float MaxValue
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

        public RandomFloat(float min, float max)
        {
            minValue = min;
            maxValue = max;
            pinnedValue = min;
        }

        public static implicit operator float(RandomFloat randomFloat)
        {
            return Random.Range(randomFloat.MinValue, randomFloat.MaxValue);
        }
    }

}