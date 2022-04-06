using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FH.Core.Architecture.Helper
{
    public static class DictionaryExtension
    {
        public static TValue TryGetValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value;
            if (!dictionary.TryGetValue(key, out value))
            {
                value = default(TValue);
            }

            return value;
        }
    }

}