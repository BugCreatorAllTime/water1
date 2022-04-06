using UnityEngine;
using System.Collections;

namespace FH.Core.Gameplay.HelperComponent
{
    public class Rotator : OutsiteTargetTransform
    {
        [SerializeField]
        Vector3 angularSpeed = new Vector3();
        [SerializeField]
        bool unscaledTime = false;

        public Vector3 AngularSpeed
        {
            get
            {
                return angularSpeed;
            }

            set
            {
                angularSpeed = value;
            }
        }

        void Update()
        {
            Rotate(AngularSpeed * (unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime));
        }
    }

}