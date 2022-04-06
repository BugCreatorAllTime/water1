using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    [System.Serializable]
    public struct WeightedMString
    {
        public string name;
        public float weight;
        public MString value;

        public static WeightedMString GetFromList(List<WeightedMString> list)
        {
            ///
            WeightedMString result = list[0];

            /// sum weights
            float sumWeight = 0;
            foreach (var item in list)
            {
                sumWeight += item.weight;
            }

            ///
            float randomNumber = Random.Range(0, sumWeight);
            float currentBound = 0;
            foreach (var item in list)
            {
                currentBound += item.weight;
                if (randomNumber <= currentBound)
                {
                    result = item;
                    break;
                }
            }

            ///
            return result;
        }
    }

}