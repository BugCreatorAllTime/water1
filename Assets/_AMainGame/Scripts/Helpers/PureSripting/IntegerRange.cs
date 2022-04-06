using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GD
{
    [System.Serializable]
    public class IntegerRange : IWeighted
    {
        public string name;
        public int min;
        public int max;
        [SerializeField]
        private float weight;

        public float Weight
        {
            get
            {
                return weight;
            }

            set
            {
                weight = value;
            }
        }

        public int Value
        {
            get
            {
                return Random.Range(min, max);
            }
        }
    }

}