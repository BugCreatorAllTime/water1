using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace FH.Core.Architecture.Pool
{
    public class Pool<T> : IPool<T>
    {
        List<T> instances = new List<T>();
        T prototype;

        #region IPool<T>
        public void PushInstance<U>(U memberInstance) where U : IPoolMember<T>, T
        {
            instances.Add(memberInstance);
            memberInstance.Pool = this;
            memberInstance.InPool = false;
        }

        public void PushPrototype<U>(U memberPrototype) where U : IPoolMember<T>, T
        {
            prototype = memberPrototype;
        }

        public T TakeInstance(bool forceCloning)
        {
            if (instances.Count > 0)
            {
                return TakeAvailableInstance();
            }
            else
            {
                if (forceCloning)
                {
                    return TakeIntanceByClonning();
                }
                else
                {
                    throw new Exception("Pool memebers are not available to take");
                }
            }
        }
        #endregion

        T TakeAvailableInstance()
        {
            int lastIndex = instances.Count - 1;
            T instance = instances[lastIndex];
            (instance as IPoolMember<T>).InPool = false;
            instances.RemoveAt(lastIndex);
            return instance;
        }

        T TakeIntanceByClonning()
        {
            T instance = (prototype as ICloneable<T>).Clone();
            (instance as IPoolMember<T>).Pool = this;
            (instance as IPoolMember<T>).InPool = false;
            return instance;
        }
    }

}