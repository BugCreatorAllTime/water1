using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GD
{
    public class ImageFillProgressBar : ProgressBar
    {
        [Header("ImageFillProgressBar")]
        [SerializeField]
        Image image;

        protected override void DisplayValue(float value)
        {
            image.fillAmount = value;
        }

        public void Reset()
        {
            image = GetComponent<Image>();
        }
    }

}