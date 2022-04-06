using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture.Pool
{
    public interface ICloneable<T>
    {
        T Clone();
    }

}