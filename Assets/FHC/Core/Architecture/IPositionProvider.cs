using UnityEngine;

namespace FH.Core.Architecture
{
    public interface IPositionProvider
    {
        Vector3 Position { get; }
    }
}