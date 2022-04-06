using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.HelperComponent
{
    public class UISetWidthByPercentage : UIRectTransformByPercentage
    {
        protected override void SetContent()
        {
            var p = targetRectTransform.sizeDelta;
            p.x = CurrentValue;
            targetRectTransform.sizeDelta = p;
        }
    }

}