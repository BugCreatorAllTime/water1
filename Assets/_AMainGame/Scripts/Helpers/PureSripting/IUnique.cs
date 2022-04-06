using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GD
{
    public interface IUnique<T>
    {
        T Id { get; }
    }

}