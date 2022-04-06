using UnityEngine;
using System.Collections;

namespace FH.Core.Gameplay.HelperComponent
{
    [RequireComponent(typeof(Renderer))]
    public class Blinker : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        Renderer targetRenderer;
        [SerializeField]
        float interval = 0.2f;

        float timeTracking = 0;

        public void Update()
        {
            timeTracking += Time.deltaTime;
            if (timeTracking >= interval)
            {
                timeTracking = 0;
                targetRenderer.enabled = !targetRenderer.enabled;
            }
        }

        public void Reset()
        {
            targetRenderer = GetComponent<Renderer>();
        }
    }

}