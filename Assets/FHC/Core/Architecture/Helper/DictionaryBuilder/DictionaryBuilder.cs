using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace FH.Core.Architecture.Helper
{
    public static class DictionaryBuilder
    {
        public static Dictionary<U, V> GetDictionary<U, V>(GameObject rootGameObject) where V : class
        {
            Dictionary<U, V> dictionary = new Dictionary<U, V>();

            IDictionaryValue<U>[] keys = rootGameObject.GetComponentsInChildren<IDictionaryValue<U>>();

            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i] is V)
                {
                    U key = keys[i].DictionaryKey;
                    if (dictionary.ContainsKey(key))
                    {
                        throw new System.Exception(string.Format("Key {0} is duplicated", key));
                    }
                    dictionary.Add(key, keys[i] as V);
                }
            }

            return dictionary;
        }
    }

}