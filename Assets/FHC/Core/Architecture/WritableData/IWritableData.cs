using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture.WritableData
{
    public interface IWritableData<T>
    {
        T Data { get; set; }
        void SaveData();
    }

}