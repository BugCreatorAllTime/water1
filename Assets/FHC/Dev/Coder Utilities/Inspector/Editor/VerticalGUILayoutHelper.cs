using UnityEngine;
using System.Collections;

namespace FH.DevTool
{
    public class VerticalGUILayout
    {
        Rect containerRect;
        float lastX;
        float lastWidth;
        bool calledFirst = false;

        public VerticalGUILayout(Rect containerRect)
        {
            this.containerRect = containerRect;
        }

        public Rect GetNextRect(float width, float spacing = 0.0f)
        {
            float currentX;

            if (calledFirst)
            {
                currentX = lastX + lastWidth;
            }
            else
            {
                currentX = containerRect.x;
                calledFirst = true;
            }

            currentX += spacing;

            lastWidth = width;
            lastX = currentX;

            return new Rect()
            {
                width = width,
                height = containerRect.height,
                x = currentX,
                y = containerRect.y
            };

        }

    }

}