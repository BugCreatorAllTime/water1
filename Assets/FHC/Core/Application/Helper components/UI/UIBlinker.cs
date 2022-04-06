using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace FH.Core.HelperComponent
{
    [RequireComponent(typeof(Graphic))]
    public class UIBlinker : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        Graphic targetRenderer;
        [SerializeField]
        float interval = 0.2f;
        [SerializeField]
        float hiddenDuration = 0.05f;
        [SerializeField]
        float delay = 0;

        [Space]
        [SerializeField]
        bool unscaledTime = false;

        public void Awake()
        {
            targetRenderer = GetComponent<Graphic>();
        }

        public void OnEnable()
        {
            StartCoroutine(Blink());
        }

        public void OnDisable()
        {
            StopAllCoroutines();
        }

        IEnumerator Blink()
        {
            ///
            if (delay > 0)
            {
                if (unscaledTime)
                {
                    yield return new WaitForSecondsRealtime(delay);
                }
                else
                {
                    yield return new WaitForSeconds(delay);
                }
            }

            ///
            while (true)
            {
                ///
                targetRenderer.enabled = true;
                if (unscaledTime)
                {
                    yield return new WaitForSecondsRealtime(interval);
                }
                else
                {
                    yield return new WaitForSeconds(interval);
                }

                ///
                targetRenderer.enabled = false;
                if (unscaledTime)
                {
                    yield return new WaitForSecondsRealtime(hiddenDuration);
                }
                else
                {
                    yield return new WaitForSeconds(hiddenDuration);
                }
            }
        }
    }

}