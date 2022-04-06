using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture.Pool
{
    public interface IPoolMemberBase<T> : ICloneable<T>
    {
        /// <summary>
        /// Should only be set by IPool
        /// </summary>
        bool InPool { get; set; }
    }

}