using UnityEngine;
using System.Collections;

namespace FH.Core
{
    public abstract class OutsiteTargetTransform : MonoBehaviour
    {
        [Header("OutsiteTargetTransform")]
        [SerializeField]
        Transform targetTransform;

        [SerializeField]
        bool useLocalPosition = false;
        [SerializeField]
        bool useLocalRotation = false;
        [SerializeField]
        bool useLocalScale = true;

        protected Transform TargetTransform
        {
            get
            {
                return targetTransform;
            }

            set
            {
                targetTransform = value;
            }
        }

        #region Protected properties
        protected Vector3 TargetPosition
        {
            get
            {
                return useLocalPosition ? TargetTransform.localPosition : TargetTransform.position;
            }

            set
            {
                if (useLocalPosition)
                {
                    TargetTransform.localPosition = value;
                }
                else
                {
                    targetTransform.position = value;
                }
            }
        }

        protected Vector3 TargetEulerRotation
        {
            get
            {
                if (useLocalRotation)
                {
                    return TargetTransform.localEulerAngles;
                }
                else
                {
                    return TargetTransform.eulerAngles;
                }
            }

            set
            {
                if (useLocalRotation)
                {
                    TargetTransform.localEulerAngles = value;
                }
                else
                {
                    TargetTransform.eulerAngles = value;
                }
            }
        }

        protected void Rotate(Vector3 eulerAngles)
        {
            targetTransform.Rotate(eulerAngles);
        }

        protected Vector3 TargetScale
        {
            get
            {
                if (useLocalScale)
                {
                    return TargetTransform.localScale;
                }
                else
                {
                    return TargetTransform.lossyScale;
                }
            }

            set
            {
                if (useLocalScale)
                {
                    TargetTransform.localScale = value;
                }
                else
                {
                    throw new System.NotSupportedException();
                }
            }
        }
        #endregion

        #region MonoB
        public void OnValidate()
        {
            if (TargetTransform == null)
            {
                FHLog.LogError("targetTransform can not be null");
            }
        }

        public void Reset()
        {
            TargetTransform = transform;
        }
        #endregion
    }

}