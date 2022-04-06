using UnityEngine;
using System.Collections;

namespace FH.Core.Gameplay.HelperComponent
{
    public class PositionOffsetAnimator : OutsiteTargetTransform
    {
        [Header("X")]
        [SerializeField]
        AnimationCurve offsetXCurve = new AnimationCurve();
        [SerializeField]
        float amplitudeX = 1.0f;

        [Header("Y")]
        [SerializeField]
        AnimationCurve offsetYCurve = new AnimationCurve();
        [SerializeField]
        float amplitudeY = 1.0f;

        [Header("Z")]
        [SerializeField]
        AnimationCurve offsetZCurve = new AnimationCurve();
        [SerializeField]
        float amplitudeZ = 1.0f;

        [Space]
        [SerializeField]
        float duration = 1;
        [SerializeField]
        bool loop = false;

        float currentTime = 0;
        float currentTime01;
        Vector3 originalPosition;
        bool animating = false;

        void OnEnable()
        {
            if (!animating)
            {
                enabled = false;
            }
        }

        void Update()
        {
            UpdateTargetPosition();
            UpdateCurrentTime();
        }

        void UpdateTargetPosition()
        {
            Vector3 offsetVector;
            offsetVector.x = GetOffset(offsetXCurve, currentTime, amplitudeX);
            offsetVector.y = GetOffset(offsetYCurve, currentTime, amplitudeY);
            offsetVector.z = GetOffset(offsetZCurve, currentTime, amplitudeZ);
            TargetPosition = originalPosition + offsetVector;
        }

        void UpdateCurrentTime()
        {
            if (currentTime >= duration)
            {
                if (loop)
                {
                    currentTime = 0;
                    currentTime01 = 0;
                }
                else
                {
                    StopAnimate();
                }
            }
            else
            {
                currentTime += Time.deltaTime;
                currentTime01 = currentTime / duration;
            }
        }

        float GetOffset(AnimationCurve curve, float time, float amplitude)
        {
            return curve.Evaluate(time) * amplitude;
        }

        [ContextMenu("Start animating")]
        public void StartAnimate()
        {
            currentTime = 0;
            originalPosition = TargetPosition;
            animating = true;
            enabled = true;
        }

        public void StopAnimate()
        {
            animating = false;
            enabled = false;
        }

        public void CreateCurve()
        {
            //offsetXCurve.AddKey(new Keyframe() {  })
        }
    }

}