using UnityEngine;
using System.Collections;

namespace FH.Core.Gameplay.HelperComponent
{
    public class GeneralOscillator : OutsiteTargetTransform
    {
        [Header("GeneralOscillator")]
        [SerializeField]
        float periodDuration = 1;
        [SerializeField]
        Vector3AnimationCurve positionCurve = Vector3AnimationCurve.Linear(0, Vector3.zero, 1, Vector3.zero);
        [SerializeField]
        bool ignoreX = false;
        [SerializeField]
        bool ignoreY = false;
        [SerializeField]
        bool ignoreZ = false;

        float timeTracking = 0;

        public float PeriodDuration
        {
            get
            {
                return periodDuration;
            }

            set
            {
                periodDuration = value;
            }
        }

        public void Update()
        {
            UpdatePosition();
            UpdateTimeTracking();
        }

        private void UpdatePosition()
        {
            var newPostion = positionCurve.Evaluate(timeTracking / PeriodDuration);
            var oldPosition = TargetPosition;
            if (ignoreX)
            {
                newPostion.x = oldPosition.x;
            }
            if (ignoreY)
            {
                newPostion.y = oldPosition.y;
            }
            if (ignoreZ)
            {
                newPostion.z = oldPosition.z;
            }
            TargetPosition = newPostion;
        }

        private void UpdateTimeTracking()
        {
            if (timeTracking != PeriodDuration)
            {
                timeTracking = Mathf.MoveTowards(timeTracking, PeriodDuration, Time.deltaTime);
            }
            else
            {
                timeTracking = 0;
            }
        }
    }

}