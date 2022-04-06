using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture.Pool
{
    public interface IMultiPrototypesPool<T>:IPoolBase<T>
    {
        void PushInstance<U>(U memberInstance) where U : T, IMultiPrototypesPoolMember<T>;
        void PushPrototype<U>(U memberPrototype) where U : T, IMultiPrototypesPoolMember<T>;
        T TakeInstance(int prototypeId, bool forceCloning);
        bool ContainsPrototype(int prototypeId);
    }

}