using UnityEngine;
using System.Collections;

namespace FH.Core.HelperComponent
{
    public class ObjectActivationSwitcher : MonoBehaviour
    {
        [SerializeField]
        GameObject targetObject;

        public void Switch()
        {
            targetObject.SetActive(!targetObject.activeSelf);
        }
    }

}