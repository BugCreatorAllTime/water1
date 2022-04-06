using UnityEngine;
using System.Collections;
using FH.Core.Architecture.UI;
using FH.Core.Architecture;

namespace FH.Core.HelperComponent
{
    public class CountDownText : TextContentSetter
    {
        [SerializeField]
        int startNumber = 3;
        [SerializeField]
        int endNumber = 1;
        [SerializeField]
        float interval = 1;
        [SerializeField]
        OrderedEventDispatcher onFinish = new OrderedEventDispatcher();

        int currentNumber = 0;
        bool counting = false;

        protected override void SetContent()
        {
            Text.text = currentNumber.ToString();
        }

        [ContextMenu("StartCountDown")]
        public void StartCountDown()
        {
            if (!counting)
            {
                StartCoroutine(CountDownAsync());
            }
        }

        public void StopCounting()
        {
            counting = false;
        }

        IEnumerator CountDownAsync()
        {
            counting = true;
            float timeTracking = 0;

            currentNumber = startNumber;
            while (currentNumber != endNumber)
            {
                if (!counting)
                {
                    break;
                }

                yield return new WaitForEndOfFrame();
                timeTracking += Time.unscaledDeltaTime;
                if (timeTracking >= interval)
                {
                    timeTracking = 0;
                    currentNumber = (int)Mathf.MoveTowards(currentNumber, endNumber, 1);
                }
            }

            timeTracking = 0;
            while (timeTracking < interval)
            {
                if (!counting)
                {
                    break;
                }
                yield return new WaitForEndOfFrame();
                timeTracking += Time.unscaledDeltaTime;
            }

            if (counting)
            {

                onFinish.Dispatch();

                counting = false;
            };
        }

        public void OnValidate()
        {
            setContentAtUpdate = true;
        }
    }

}