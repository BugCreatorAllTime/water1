using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture
{
    public abstract class Singleton<T> where T : new()
    {
        static bool instantiated = false;
        static T instance;

        public static T Instance
        {
            get
            {
                if (!instantiated)
                {
                    instance = new T();
                    instantiated = true;
                }
                return instance;
            }
        }
    }

    
}