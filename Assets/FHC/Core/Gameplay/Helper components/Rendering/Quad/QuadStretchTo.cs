using UnityEngine;
using System.Collections;

namespace FH.Core.Gameplay.HelperComponent
{
    [RequireComponent(typeof(QuadStretcher))]
    public class QuadStretchTo : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        QuadStretcher quadStretcher;

        [SerializeField]
        Vector4 targetStretch;
        [SerializeField]
        float duration;

        bool stopFlag = false;
        bool stretching = false;

        [ContextMenu("Stretch")]
        public void Stretch()
        {
            if (duration == 0)
            {
                quadStretcher.Stretch = targetStretch;
            }
            else
            {
                StartCoroutine(StretchAsync());
            }
        }

        public void StopStretching()
        {
            if (stretching)
            {
                stopFlag = true;
            }
        }

        IEnumerator StretchAsync()
        {
            stretching = true;

            float t = 0;
            Vector4 startStretch = quadStretcher.Stretch;
            while (t <= duration)
            {
                if (stopFlag)
                {
                    stopFlag = false;
                    break;
                }

                quadStretcher.Stretch = Vector4.Lerp(startStretch, targetStretch, t / duration);
                t += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            stretching = false;
        }

        public void Reset()
        {
            quadStretcher = GetComponent<QuadStretcher>();
        }
    }

}