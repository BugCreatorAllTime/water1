using UnityEngine;
using System.Collections;

namespace FH.Core.HelperComponent
{
    [RequireComponent(typeof(Camera))]
    public class CullAllCameraEventMask : MonoBehaviour
    {
        public void Update()
        {
            Camera cam = GetComponent<Camera>();            
            cam.eventMask = 0;
        }
    }

}