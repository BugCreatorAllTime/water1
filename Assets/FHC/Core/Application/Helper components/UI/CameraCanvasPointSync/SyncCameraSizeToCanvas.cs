using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace FH.Core.HelperComponent
{
    [ExecuteInEditMode]
    public class SyncCameraSizeToCanvas : MonoBehaviour
    {
        [SerializeField]
        RectTransform canvasRectTransform;
        [SerializeField]
        Camera targetCamera;    
             
        public void LateUpdate()
        {
            targetCamera.orthographicSize = canvasRectTransform.sizeDelta.y * canvasRectTransform.lossyScale.y / 2.0f;
        }

    }

}