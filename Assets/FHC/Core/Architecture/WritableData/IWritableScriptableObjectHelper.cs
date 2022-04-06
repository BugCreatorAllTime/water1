using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture.WritableData
{
    /// <summary>
    /// For editor use only
    /// </summary>
    public interface IWritableScriptableObjectHelper
    {
        void Editor_LoadCurrentData();
    }

}