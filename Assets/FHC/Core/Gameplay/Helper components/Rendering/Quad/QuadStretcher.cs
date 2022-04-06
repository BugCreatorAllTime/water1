using UnityEngine;
using System.Collections;

namespace FH.Core.Gameplay.HelperComponent
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class QuadStretcher : MonoBehaviour
    {
        [SerializeField]
        Vector3 originalLocalPosition;
        [SerializeField]
        float left;
        [SerializeField]
        float right;
        [SerializeField]
        float top;
        [SerializeField]
        float bottom;

        public Vector4 Stretch
        {
            get
            {
                return new Vector4(left, right, top, bottom);
            }
            set
            {
                left = value.x;
                right = value.y;
                top = value.z;
                bottom = value.w;
            }
        }

        public Vector3 OriginalLocalPosition
        {
            get
            {
                return originalLocalPosition;
            }

            set
            {
                originalLocalPosition = value;
            }
        }

        public Vector3 OriginalWorldPosition
        {
            get
            {
                return transform.TransformPoint(originalLocalPosition);
            }
        }

        public float Left
        {
            get
            {
                return left;
            }

            set
            {
                left = value;
            }
        }

        public float Right
        {
            get
            {
                return right;
            }

            set
            {
                right = value;
            }
        }

        public float Top
        {
            get
            {
                return top;
            }

            set
            {
                top = value;
            }
        }

        public float Bottom
        {
            get
            {
                return bottom;
            }

            set
            {
                bottom = value;
            }
        }

        public void UpdateTransform()
        {
            float scaleX = Left + Right;
            float scaleY = Top + Bottom;
            float posX = OriginalLocalPosition.x - Left + scaleX / 2.0f;
            float posY = OriginalLocalPosition.y - Bottom + scaleY / 2.0f;
            transform.localPosition = new Vector3(posX, posY, OriginalLocalPosition.z);
            transform.localScale = new Vector3(scaleX, scaleY, 1);
        }
                
        public void SetToZero()
        {
            Stretch = Vector4.zero;
        }

        public void LateUpdate()
        {
            UpdateTransform();
        }

        public void Reset()
        {
            originalLocalPosition = transform.localPosition;
            left = transform.localScale.x / 2.0f;
            right = transform.localScale.x / 2.0f;
            top = transform.localScale.y / 2.0f;
            bottom = transform.localScale.y / 2.0f;
        }
    }

}