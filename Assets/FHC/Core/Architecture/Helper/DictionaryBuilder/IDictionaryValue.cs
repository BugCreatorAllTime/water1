using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture.Helper
{
    public interface IDictionaryValue<T>
    {
        T DictionaryKey { get; }
    }

}