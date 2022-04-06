using UnityEngine;
using System.Collections;
using FH.Core.Architecture;

namespace FH.Core.HelperComponent
{
    public class UIMoveTo : OutsiteTargetRectTransform
    {
        [SerializeField]
        OrderedEventDispatcher onFinishMoving;

        [Space]
        [SerializeField]
        Vector3 desireTargetPosition;
        [SerializeField]
        float duration = 0.5f;
        [SerializeField]
        bool useX;
        [SerializeField]
        bool useY;
        [SerializeField]
        bool useZ;

        public void MoveImmediately()
        {
            Vector3 startPosition = TargetRectTransform.anchoredPosition;
            Vector3 targetPosition = GetFinalTargetPosition(startPosition);
            TargetRectTransform.anchoredPosition = targetPosition;
        }

        [ContextMenu("Move")]
        public void Move()
        {
            if (duration > 0)
            {
                StartCoroutine(MoveAsync());
            }
            else
            {
                TargetRectTransform.anchoredPosition = GetFinalTargetPosition(TargetRectTransform.anchoredPosition);
                onFinishMoving.Dispatch();
            }
        }

        IEnumerator MoveAsync()
        {
            float currentTime = 0;

            Vector3 startPosition = TargetRectTransform.anchoredPosition;
            Vector3 targetPosition = GetFinalTargetPosition(startPosition);

            while (currentTime <= duration)
            {
                currentTime = Mathf.MoveTowards(currentTime, duration, Time.unscaledDeltaTime);
                TargetRectTransform.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, currentTime / duration);
                if (currentTime == duration)
                {
                    break;
                }
                yield return new WaitForEndOfFrame();
            }
            onFinishMoving.Dispatch();
            yield return null;
        }

        private Vector3 GetFinalTargetPosition(Vector3 startPositon)
        {
            if (useX)
            {
                startPositon.x = desireTargetPosition.x;
            }
            if (useY)
            {
                startPositon.y = desireTargetPosition.y;
            }
            if (useZ)
            {
                startPositon.z = desireTargetPosition.z;
            }

            return startPositon;
        }

        [ContextMenu("UseCurrentPositionAsDesired")]
        void UseCurrentPositionAsDesired()
        {
            desireTargetPosition = TargetRectTransform.anchoredPosition;
#if UNITY_EDITOR
            UnityEditor.Undo.RecordObject(this, "UseCurrentPositionAsTarget");
            UnityEditor.Undo.FlushUndoRecordObjects();
#endif
        }
    }

}