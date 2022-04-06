using UnityEngine;
using System.Collections.Generic;
using System;

namespace FH.Core.Gameplay.Helper
{
    public static class TransformHelper
    {
        #region Set single vector component
        public static void SetLocalPositionX(this Transform transform, float newX)
        {
            var p = transform.localPosition;
            p.x = newX;
            transform.localPosition = p;
        }

        public static void SetLocalPositionY(this Transform transform, float newY)
        {
            var p = transform.localPosition;
            p.y = newY;
            transform.localPosition = p;
        }

        public static void SetLocalScaleX(this Transform transform, float newX)
        {
            var p = transform.localScale;
            p.x = newX;
            transform.localScale = p;
        }

        public static void SetLocalScaleY(this Transform transform, float newY)
        {
            var p = transform.localScale;
            p.y = newY;
            transform.localScale = p;
        }
        #endregion

        #region Set 2 vector components
        public static void SetLocalPositionXY(this Transform transform, Vector2 position)
        {
            var p = transform.localPosition;
            p.x = position.x;
            p.y = position.y;
            transform.localPosition = p;
        }

        public static void SetPositionXY(this Transform transform, Vector2 position)
        {
            var p = transform.position;
            p.x = position.x;
            p.y = position.y;
            transform.position = p;
        }
        #endregion

        #region Comparer
        public class TransformSilblingIndexComparer : IComparer<Transform>
        {
            public int Compare(Transform x, Transform y)
            {
                int sx = x.GetSiblingIndex();
                int sy = y.GetSiblingIndex();
                if (sx > sy)
                {
                    return 1;
                }
                if (sx < sy)
                {
                    return -1;
                }
                return 0;
            }
        }

        public class TransformPositionXComparer : IComparer<Transform>
        {
            public int Compare(Transform x, Transform y)
            {
                float posX = x.position.x;
                float posY = y.position.x;
                if (posX > posY)
                {
                    return 1;
                }
                if (posX < posY)
                {
                    return -1;
                }
                return 0;
            }
        }

        public class TransformPositionYComparer : IComparer<Transform>
        {
            public int Compare(Transform x, Transform y)
            {
                float posX = x.position.y;
                float posY = y.position.y;
                if (posX > posY)
                {
                    return 1;
                }
                if (posX < posY)
                {
                    return -1;
                }
                return 0;
            }
        }
        #endregion
    }



}