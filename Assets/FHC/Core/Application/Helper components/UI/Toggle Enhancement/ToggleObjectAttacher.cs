using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace SR.Core.HelperComponent
{
    [RequireComponent(typeof(Toggle))]
    [ExecuteInEditMode]
    public class ToggleObjectAttacher : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        Toggle toggle;

        [SerializeField]
        GameObject attachedObject;

        public void Awake()
        {
            if (toggle == null)
            {
                toggle = GetComponent<Toggle>();
            }
            SetAttachedObjectActive(toggle.isOn);
            toggle.onValueChanged.AddListener((bool isOn) => { SetAttachedObjectActive(isOn); });
        }

#if UNITY_EDITOR
        public void Update()
        {
            if (!Application.isPlaying)
            {
                SetAttachedObjectActive(toggle.isOn);
            }
        }
#endif

        void SetAttachedObjectActive(bool active)
        {
            if (attachedObject != null)
            {
                attachedObject.SetActive(active);
            }
        }
        
        void Reset()
        {
            toggle = GetComponent<Toggle>();
        }
    }

}