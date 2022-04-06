using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GD
{
    [System.Serializable]
    public class WeightedInt : IWeighted
    {
        [SerializeField]
        int value;
        [SerializeField]
        float weight;

        public int Value { get => value; set => this.value = value; }
        public float Weight { get => weight; set => weight = value; }
    }

    [System.Serializable]
    public class WeightedFloat : IWeighted
    {
        [SerializeField]
        float value;
        [SerializeField]
        float weight;

        public float Value { get => value; set => this.value = value; }
        public float Weight { get => weight; set => weight = value; }
    }

}