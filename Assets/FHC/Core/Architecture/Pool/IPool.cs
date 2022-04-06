using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture.Pool
{
    public interface IPool<T> : IPoolBase<T>
    {
        void PushInstance<U>(U memberInstance) where U : T, IPoolMember<T>;
        void PushPrototype<U>(U memberPrototype) where U : T, IPoolMember<T>;
        T TakeInstance(bool forceCloning);

    }

}