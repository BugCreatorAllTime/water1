using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

namespace FH.Core.Gameplay.HelperComponent
{
    public class TransformRootSetter : MonoBehaviour
    {
        [SerializeField]
        Transform targetTransform;
        [SerializeField]
        bool setAtAwake = true;

        public void Awake()
        {
            if (setAtAwake)
            {
                Set();
            }
        }

        public void Set()
        {
            targetTransform.parent = null;
        }

        public void OnValidate()
        {
            Assert.IsTrue(targetTransform != null);
        }

        public void Reset()
        {
            targetTransform = transform;
        }
    }

}