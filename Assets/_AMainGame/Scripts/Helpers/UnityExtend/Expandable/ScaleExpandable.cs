using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleExpandable : Expandable
{
    [Header("ScaleExpandable")]
    [SerializeField]
    private Vector2 sizeAtScaleOne = Vector2.one;

    public override Vector2 Size
    {
        get
        {
            var scale = transform.localScale;
            return new Vector2(scale.x * sizeAtScaleOne.x, scale.y * sizeAtScaleOne.y);
        }
    }

    public override void Expand(float left, float right, float top, float bottom)
    {
        ///
        var scale = transform.localScale;

        ///
        var newPos = GetNewPos(transform.localPosition, scale.x * sizeAtScaleOne.x, scale.y * sizeAtScaleOne.y, left, right, top, bottom);

        ///
        transform.localScale = scale + new Vector3((left + right) / sizeAtScaleOne.x, (top + bottom) / sizeAtScaleOne.y);
        transform.localPosition = newPos;
    }
}
