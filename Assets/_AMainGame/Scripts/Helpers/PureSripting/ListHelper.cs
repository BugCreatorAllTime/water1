using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GD
{
    public static class ListHelper
    {
        public delegate T GetObjectUniqueIdFunction<T, U>(U obj);

        public static Dictionary<T, U> ToDictionary<T, U>(this List<U> list, GetObjectUniqueIdFunction<T, U> getObjectUniqueIdFunction)
        {
            Dictionary<T, U> dictionary = new Dictionary<T, U>();

            foreach (var item in list)
            {
                dictionary.Add(getObjectUniqueIdFunction(item), item);
            }

            return dictionary;
        }

        public static Dictionary<T, U> ToDictionary<T, U>(this List<U> list) where U : IUnique<T>
        {
            return list.ToDictionary((U u) => { return u.Id; });
        }

        /// <summary>
        ///Null list to an empty list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void CorrectNullList<T>(ref List<T> list)
        {
            if (list == null)
            {
                list = new List<T>();
            }
        }

        /// <summary>
        /// Get random item, delete it in the list and return value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T TakeRandomItem<T>(this List<T> list)
        {
            ///
            if (list.Count <= 0)
            {
                throw new System.Exception();
            }

            ///
            int index = Random.Range(0, list.Count);
            T item = list[index];
            list.RemoveAt(index);

            ///
            return item;
        }

        /// <summary>
        /// Get random item without remove it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static T GetRandomItem<T>(this T[] ts)
        {
            ///
            if (ts.Length == 0)
            {
                throw new System.Exception();
            }

            ///
            return ts[Random.Range(0, ts.Length)];
        }

        /// <summary>
        /// Get random item without removing it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static T GetRandomItem<T>(this List<T> ts)
        {
            ///
            if (ts.Count == 0)
            {
                throw new System.Exception();
            }

            ///
            return ts[Random.Range(0, ts.Count)];
        }

        public static IEnumerable<T> GetRandomItems<T>(this IList<T> ts, int count)
        {
            ///
            if (ts == null)
            {
                throw new System.Exception();
            }

            ///
            if (count == 0)
            {
                yield break;
            }

            ///
            if (ts.Count < count)
            {
                throw new System.Exception();
            }

            ///
            int interval = ts.Count / count;

            ///
            int startId = 0;
            for (int i = 0; i < count; i++)
            {
                ///
                int endId = (i == count - 1) ? ts.Count - 1 : startId + interval - 1;
                var pickedId = Random.Range(startId, endId + 1);

                ///
                yield return ts[pickedId];

                ///
                startId = endId + 1;
            }
        }

        /// <summary>
        /// Move data from srcList to desList in a random order
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="srcList"></param>
        /// <param name="desList"></param>
        /// <param name="forceNull"></param>
        public static void ShuffleMoveList<T>(ref List<T> srcList, ref List<T> desList, bool forceNull = false)
        {
            ///
            if (srcList == null)
            {
                ///
                if (forceNull)
                {
                    desList = null;
                }
                else
                {
                    desList?.Clear();
                }

                ///
                return;
            }

            ///
            if (desList == null)
            {
                desList = new List<T>();
            }
            else
            {
                desList.Clear();
            }

            ///
            while (srcList.Count > 0)
            {
                ///
                var value = srcList.TakeRandomItem();

                ///
                desList.Add(value);
            }
        }

        public static void CopyList<T>(ref List<T> srcList, ref List<T> desList, bool forceNull = false)
        {
            ///
            if (srcList == null)
            {
                ///
                if (forceNull)
                {
                    desList = null;
                }
                else
                {
                    desList?.Clear();
                }

                ///
                return;
            }

            ///
            if (desList == null)
            {
                desList = new List<T>();
            }
            else
            {
                desList.Clear();
            }

            ///
            for (int i = 0; i < srcList.Count; i++)
            {
                desList.Add(srcList[i]);
            }
        }

        public static bool IsNullOrEmpty<T>(List<T> list)
        {
            ///
            if (list == null)
            {
                return true;
            }

            ///
            if (list.Count == 0)
            {
                return true;
            }

            ///
            return false;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void Shuffle<T>(this IList<T> list, int startId, int endId)
        {
            int n = endId + 1;
            while (n > (startId + 1))
            {
                n--;
                int k = Random.Range(startId, endId + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
