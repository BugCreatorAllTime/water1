using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FH.Core.Gameplay.HelperComponent
{
    public class MoveRandomInCircle : MonoBehaviour
    {
        [SerializeField]
        Vector2 center;
        [SerializeField]
        float radius = 1;
        [SerializeField]
        float speed = 2;
        [SerializeField]
        bool unscaledTime;

        public void OnEnable()
        {
            StartCoroutine(MoveLoop());
        }

        public void OnDisable()
        {
            StopAllCoroutines();
        }

        IEnumerator MoveLoop()
        {
            ///
            speed = Mathf.Abs(speed);

            ///
            while (true)
            {
                // Random target
                var target = Random.insideUnitCircle * radius + center;
                var distance = Vector2.Distance(transform.localPosition, target);
                var duration = distance / speed;

                ///
                yield return StartCoroutine(MoveTo(target, duration));
            }
        }

        IEnumerator MoveTo(Vector2 target, float duration)
        {
            float t = 0;
            while (t < duration)
            {
                ///
                yield return null;

                ///
                var deltaTime = unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
                t += deltaTime;

                ///
                var currentPos = transform.localPosition;
                var newPos = (Vector3)Vector2.MoveTowards(currentPos, target, speed * deltaTime);
                newPos.z = currentPos.z;

                ///
                transform.localPosition = newPos;
            }
        }
    }
}
