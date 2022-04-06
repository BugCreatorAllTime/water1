using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GD
{
    public interface IMultiGraphicTransition
    {
        List<Graphic> AdditionalTargetGraphics { get; }
    }

}