using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMod
{
    public class UnityThreadHelper : MonoBehaviour
    {
        private static UnityThreadHelper instance;

        public static UnityThreadHelper Instance
        {
            get
            {
                ///
                if (instance == null)
                {
                    var tmpObj = Instantiate(new GameObject("UnityThreadHelper", typeof(UnityThreadHelper)));
                }

                ///
                return instance;
            }
        }

        List<System.Action> callbacks = new List<System.Action>();

        public void Awake()
        {
            ///
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            ///
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void DispatchToUnityThread(System.Action callback)
        {
            if (callback != null)
            {
                lock (this)
                {
                    callbacks.Add(callback);
                }
            }
        }

        public void LateUpdate()
        {
            ///
            foreach (var item in callbacks)
            {
                item();
            }

            ///
            callbacks.Clear();
        }
    }

}