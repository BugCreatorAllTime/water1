using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Expandable : MonoBehaviour
{
    public abstract Vector2 Size { get; }
    public abstract void Expand(float left, float right, float top, float bottom);

    protected static Vector3 GetNewPos(Vector3 currentPos, float currentWidth, float currentHeight, float leftExpansion, float rightExpansion, float topExpansion, float bottomExpansion)
    {
        ///
        float minX = -currentWidth / 2.0f - leftExpansion;
        float maxX = currentWidth / 2.0f + rightExpansion;
        float minY = -currentHeight / 2.0f - bottomExpansion;
        float maxY = currentHeight / 2.0f + topExpansion;

        ///
        Vector3 newPos = new Vector3()
        {
            x = (minX + maxX) / 2.0f,
            y = (minY + maxY) / 2.0f,
            z = 0
        };

        ///
        return newPos + currentPos;
    }
}
