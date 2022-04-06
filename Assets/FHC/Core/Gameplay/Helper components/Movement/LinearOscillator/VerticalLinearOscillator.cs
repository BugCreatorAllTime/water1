using UnityEngine;
using System.Collections;

namespace FH.Core.Gameplay.HelperComponent
{
    public class VerticalLinearOscillator : OutsiteTargetTransform
    {
        [SerializeField]
        float upDistance = 1;
        [SerializeField]
        float downDistance = 1;
        [SerializeField]
        float duration = 1;
        [SerializeField]
        bool upwardFirst = true;

        bool upward;
        float speed;
        float maxY;
        float minY;

        public float UpDistance
        {
            get
            {
                return upDistance;
            }

            set
            {
                upDistance = value;
            }
        }

        public float DownDistance
        {
            get
            {
                return downDistance;
            }

            set
            {
                downDistance = value;
            }
        }

        public float Duration
        {
            get
            {
                return duration;
            }

            set
            {
                duration = value;
            }
        }

        public bool UpwardFirst
        {
            get
            {
                return upwardFirst;
            }

            set
            {
                upwardFirst = value;
            }
        }

        void Awake()
        {
            ResetState();
        }

        public void Update()
        {
            Vector3 targetPosition = TargetPosition;
            float targetY = targetPosition.y;
            if (upward)
            {
                targetY = Mathf.MoveTowards(targetY, maxY, speed * Time.deltaTime);
                if (targetY == maxY)
                {
                    upward = false;
                }
            }
            else
            {
                targetY = Mathf.MoveTowards(targetY, minY, speed * Time.deltaTime);
                if (targetY == minY)
                {
                    upward = true;
                }
            }
            targetPosition.y = targetY;
            TargetPosition = targetPosition;
        }

        public void ResetState()
        {
            float targetY = TargetPosition.y;

            upward = UpwardFirst;
            speed = (UpDistance + DownDistance) / Duration;
            maxY = targetY + UpDistance;
            minY = targetY - DownDistance;
        }

    }
}