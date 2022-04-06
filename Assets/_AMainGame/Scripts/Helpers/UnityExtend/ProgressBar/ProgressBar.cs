using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GD
{
    public abstract class ProgressBar : MonoBehaviour
    {
        [Header("ProgressBar")]
        [SerializeField]
        protected bool inverted;

        public void SetValue(float value)
        {
            if (inverted)
            {
                DisplayValue(1 - Mathf.Clamp01(value));
            }
            else
            {
                DisplayValue(Mathf.Clamp01(value));
            }
        }

        protected abstract void DisplayValue(float value);
    }

}