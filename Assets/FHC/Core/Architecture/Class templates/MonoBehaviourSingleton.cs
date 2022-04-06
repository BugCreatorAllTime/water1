using UnityEngine;
using System.Collections;
using FH.Core.Architecture;

namespace FH.Core.Architecture
{
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour, IFirstWakeComponent where T : class
    {
        static T instance = default(T);
        static MonoBehaviourSingleton<T> currentInstance = null;
        static bool instantiated = false;
        public static T Instance
        {
            get
            {
                if (!instantiated)
                {
                    return null;
                }
                return instance;
            }
        }

        protected abstract T GetInstance();

        #region IFirstWakeComponent

        bool awoke = false;

        public bool Awoke
        {
            get
            {
                return awoke;
            }

            set
            {
                awoke = value;
            }
        }

        public virtual void FirstAwake()
        {
            if (instantiated)
            {
                FHLog.LogWarning("A MonoBehaviourSingleton is instantiated while current instance is still alive.");
            }

            instance = GetInstance();
            instantiated = true;
            currentInstance = this;
        }
        #endregion

        public virtual void OnDestroy()
        {
            if (currentInstance == this)
            {
                instance = default(T);
                instantiated = false;
            }
        }
    }

}