using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture.Pool
{
    public interface IResetableObject
    {
        void SaveCurrentState();
        void ResetToLastSavedState();
    }

}