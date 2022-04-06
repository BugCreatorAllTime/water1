using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

namespace FH.Core.HelperComponent
{
    public abstract class OutsiteTargetRectTransform : MonoBehaviour
    {
        [SerializeField]
        RectTransform targetRectTransform;        

        protected RectTransform TargetRectTransform
        {
            get
            {
                return targetRectTransform;
            }

            private set
            {
                targetRectTransform = value;
            }
        }

        public void Reset()
        {
            TargetRectTransform = GetComponent<RectTransform>();
        }

        public void OnValidate()
        {
            Assert.IsTrue(TargetRectTransform != null);
        }
    }

}