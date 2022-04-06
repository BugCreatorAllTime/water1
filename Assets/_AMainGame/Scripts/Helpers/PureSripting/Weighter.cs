using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GD
{
    public static class Weighter
    {
        /// <summary>
        /// Not remove the picked item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="weightedList"></param>
        /// <returns></returns>
        public static T PickOneIn<T>(params T[] weightedList) where T : IWeighted
        {
            ///
            if (weightedList.Length == 0)
            {
                throw new System.ArgumentException();
            }

            ///
            float weightSum = 0;
            for (int i = 0; i < weightedList.Length; i++)
            {
                weightSum += weightedList[i].Weight;
            }

            ///
            float randomValue = Random.Range(0, weightSum);

            ///           
            float currentWeight = 0;
            for (int i = 0; i < weightedList.Length; i++)
            {
                currentWeight += weightedList[i].Weight;
                if (currentWeight > randomValue || i == (weightedList.Length - 1))
                {
                    return weightedList[i];
                }
            }

            ///
            throw new System.Exception();
        }

        /// <summary>
        /// Not remove the picked item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="weightedList"></param>
        /// <returns></returns>
        public static T PickOne<T>(this List<T> weightedList) where T : IWeighted
        {
            ///
            if (weightedList.Count == 0)
            {
                throw new System.ArgumentException();
            }

            ///
            float weightSum = 0;
            for (int i = 0; i < weightedList.Count; i++)
            {
                weightSum += weightedList[i].Weight;
            }

            ///
            float randomValue = Random.Range(0, weightSum);

            ///           
            float currentWeight = 0;
            for (int i = 0; i < weightedList.Count; i++)
            {
                currentWeight += weightedList[i].Weight;
                if (currentWeight >= randomValue || i == (weightedList.Count - 1))
                {
                    return weightedList[i];
                }
            }

            ///
            throw new System.Exception();
        }
    }

}