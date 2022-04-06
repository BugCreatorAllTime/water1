using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace FH.Core.HelperComponent
{
    [RequireComponent(typeof(Toggle))]
    [ExecuteInEditMode]
    public class ToggleObjectSwaper : MonoBehaviour
    {
        [SerializeField]
        SwapObject onObject = new SwapObject();
        [SerializeField]
        SwapObject offObject = new SwapObject();

        [SerializeField, HideInInspector]
        Toggle toggle;

        void Update()
        {
            if (toggle.isOn)
            {
                onObject.SetActive(true);
                offObject.SetActive(false);
                toggle.targetGraphic = onObject.targetGraphic;
            }
            else
            {
                onObject.SetActive(false);
                offObject.SetActive(true);
                toggle.targetGraphic = offObject.targetGraphic;
            }
        }

        void Reset()
        {
            toggle = GetComponent<Toggle>();
        }

        [System.Serializable]
        class SwapObject
        {
            [SerializeField]
            public GameObject mainObject;
            [SerializeField]
            public Graphic targetGraphic;

            public void SetActive(bool active)
            {
                if (mainObject != null)
                {
                    mainObject.SetActive(active);
                }
            }
        }
    }

}