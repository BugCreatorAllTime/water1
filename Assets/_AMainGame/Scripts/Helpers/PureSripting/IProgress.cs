using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GD
{
    public interface IProgress
    {
        long StartTime { get; }
        float CompleteTime { get; }
    }
}
