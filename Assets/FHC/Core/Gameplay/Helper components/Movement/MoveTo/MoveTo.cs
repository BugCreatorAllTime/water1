using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace FH.Core.Gameplay.HelperComponent
{
    public class MoveTo : OutsiteTargetTransform
    {
        [SerializeField]
        float duration;
        [SerializeField]
        PositionProvider destination;
        [SerializeField]
        bool zStay = false;

        [Space]
        [SerializeField]
        UnityEvent onComplete = new UnityEvent();

        bool moving = false;

        public void StartMove()
        {
            ///
            if (moving)
            {
                return;
            }

            ///
            StartCoroutine(Move());
        }

        IEnumerator Move()
        {
            ///         
            moving = true;

            ///
            var startPos = TargetTransform.position;

            ///
            float t = 0;
            while (t <= duration)
            {
                ///
                t += Time.deltaTime;

                ///
                var newPos = Vector3.Lerp(startPos, destination.Position, t / duration);

                ///
                if (zStay)
                {
                    newPos.z = startPos.z;
                }

                ///
                TargetTransform.position = newPos;

                ///
                yield return null;
            }

            ///
            onComplete.Invoke();

            ///
            moving = false;
        }

    }

}