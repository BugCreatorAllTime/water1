using UnityEngine;
using System.Collections;

namespace FH.Core.HelperComponent
{
    public class UIMoveBy : OutsiteTargetRectTransform
    {
        [SerializeField]
        Vector2 distance;
        [SerializeField]
        float duration;

        [ContextMenu("Move")]
        public void Move()
        {
            StartCoroutine(MoveAsync(distance));
        }

        [ContextMenu("MoveReverse")]
        public void MoveReverse()
        {
            StartCoroutine(MoveAsync(-distance));
        }

        IEnumerator MoveAsync(Vector2 distance)
        {
            float t = 0;

            Vector3 start = TargetRectTransform.anchoredPosition;
            Vector2 target = TargetRectTransform.anchoredPosition + distance;

            while (t <= duration)
            {
                TargetRectTransform.anchoredPosition = Vector2.Lerp(start, target, t / duration);
                t = Mathf.MoveTowards(t, duration, Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            yield return null;
        }
    }
}
