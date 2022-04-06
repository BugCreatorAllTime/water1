using UnityEngine;
using System.Collections;
using FH.Core.Architecture.UI;

namespace FH.Core.HelperComponent
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class UIRectTransformByPercentage : ContentSetter
    {
        [SerializeField, HideInNormalInspector]
        protected RectTransform targetRectTransform;

        [SerializeField]
        protected float minValue;
        [SerializeField]
        protected float maxValue;

        [Space]
        [SerializeField, Range(0, 1)]
        float currentPercentage = 0.5f;

        public float CurrentPercentage
        {
            get
            {
                return currentPercentage;
            }

            set
            {
                currentPercentage = value;
            }
        }

        protected float CurrentValue
        {
            get
            {
                return minValue + (maxValue - minValue) * currentPercentage;
            }
        }

        #region MonoB

        public void OnDrawGizmos()
        {
            SetContent();
        }

        public void Reset()
        {
            targetRectTransform = GetComponent<RectTransform>();
        }
        #endregion

        #region ContentSetter
        protected override abstract void SetContent();
        #endregion

    }

}