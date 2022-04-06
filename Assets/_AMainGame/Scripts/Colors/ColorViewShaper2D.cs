using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shaper2D))]
public class ColorViewShaper2D : ColorView
{   
    protected override void SetColor(Color color)
    {
        var shaper2D = GetComponent<Shaper2D>();
        shaper2D.innerColor = shaper2D.outerColor = color;
    }
}
