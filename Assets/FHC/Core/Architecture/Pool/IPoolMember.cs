using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture.Pool
{
    public interface IPoolMember<T> : IPoolMemberBase<T>, ICloneable<T>
    {
        /// <summary>
        /// Should only be set by IPool
        /// </summary>
        IPool<T> Pool { get; set; }
        
    }

}