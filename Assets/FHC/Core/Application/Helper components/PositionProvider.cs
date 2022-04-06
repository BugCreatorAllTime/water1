using UnityEngine;
using System.Collections;
using FH.Core.Architecture;

namespace FH.Core
{
    public abstract class PositionProvider : MonoBehaviour, IPositionProvider
    {
        public abstract Vector3 Position { get; }
    }
}
