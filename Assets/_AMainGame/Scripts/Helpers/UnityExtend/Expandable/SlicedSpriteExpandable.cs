using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SlicedSpriteExpandable : Expandable
{
    public override Vector2 Size
    {
        get
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            return spriteRenderer.size;
        }
    }

    public override void Expand(float left, float right, float top, float bottom)
    {
        ///
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.drawMode = SpriteDrawMode.Sliced;

        ///
        var currentSize = spriteRenderer.size;

        ///
        var newPos = GetNewPos(transform.localPosition, currentSize.x, currentSize.y, left, right, top, bottom);

        ///
        spriteRenderer.size = currentSize + new Vector2(left + right, top + bottom);

        ///
        transform.localPosition = newPos;
    }
}
