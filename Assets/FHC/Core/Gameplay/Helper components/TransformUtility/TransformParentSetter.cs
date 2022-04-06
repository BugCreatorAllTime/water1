using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FH.Core.Gameplay.HelperComponent
{
    public class TransformParentSetter : MonoBehaviour
    {
        [SerializeField]
        Transform targetTransform;

        [Header("TransformParentSetter")]
        [SerializeField]
        List<Transform> parentsList = new List<Transform>();

        public void SetToRoot()
        {
            targetTransform.SetParent(null, true);
        }

        public void SetToRootWithoutChangingTransform()
        {
            targetTransform.SetParent(null, false);
        }

        public void SetParent(int index)
        {
            targetTransform.SetParent(parentsList[index], true);
        }
        public void SetParentWithoutChangingTransform(int index)
        {
            targetTransform.SetParent(parentsList[index], false);
        }

    }

}