using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture.Pool
{
    public interface IMultiPrototypesPoolMember<T> : IPoolMemberBase<T>
    {
        /// <summary>
        /// Should only be set by IPool
        /// </summary>
        IMultiPrototypesPool<T> Pool { get; set; }
        int PrototypeId { get; set; }
    }
}
