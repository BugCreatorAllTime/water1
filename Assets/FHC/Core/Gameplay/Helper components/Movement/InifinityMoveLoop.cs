using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FH.Core.Gameplay.HelperComponent
{
    public class InifinityMoveLoop : MonoBehaviour
    {
        [SerializeField]
        Vector3 moveBy;
        [SerializeField]
        float duration;

        Vector3 startPosition;

        float timeTracker;

        public void Awake()
        {
            startPosition = transform.localPosition;
        }

        public void OnEnable()
        {
            timeTracker = 0;
        }

        public void Update()
        {
            ///
            timeTracker += Time.deltaTime;

            ///
            timeTracker = Mathf.Repeat(timeTracker, duration);

            ///
            var targetPos = startPosition + moveBy;
            var currentPos = Vector3.Lerp(startPosition, targetPos, timeTracker / duration);

            ///
            transform.localPosition = currentPos;
        }
    }
}