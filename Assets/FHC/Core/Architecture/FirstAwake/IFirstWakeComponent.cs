using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture
{
    public interface IFirstWakeComponent
    {
        void FirstAwake();
        bool Awoke { get; set; }
    }

}