using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FH.Core.Architecture.Helper
{
    public static class DictionaryListConverter
    {
        public static void DictionaryToListWithKeysAtList<T>(List<string> keys, List<T> values, Dictionary<string, T> dictionary)
        {
            values.Clear();
            for (int i = 0; i < keys.Count; i++)
            {
                string key = keys[i];
                if (dictionary.ContainsKey(key))
                {
                    values.Add(dictionary[key]);
                }
            }
        }

        public static void DictionaryToList<K, V>(List<K> keys, List<V> values, Dictionary<K, V> dictionary)
        {
            keys.Clear();
            values.Clear();
            foreach (K key in dictionary.Keys)
            {
                keys.Add(key);
                values.Add(dictionary[key]);
            }
        }

        public static void ListToDictionary<K, V>(List<K> keys, List<V> values, Dictionary<K, V> dictionary)
        {
            dictionary.Clear();
            int numberOfValues = values.Count;
            for (int i = 0; i < keys.Count; i++)
            {
                K key = keys[i];

                if (dictionary.ContainsKey(key))
                {
                    continue;
                }

                if (i < values.Count)
                {
                    dictionary[key] = values[i];
                }
                else
                {
                    dictionary[key] = default(V);
                }
            }
        }

    }

}