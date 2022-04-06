using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Gameplay.HelperComponent
{
    public class LinearMover : OutsiteTargetTransform
    {
        [SerializeField]
        Vector3 velocity;
        public Vector3 Velocity
        {
            get
            {
                return velocity;
            }

            set
            {
                velocity = value;
            }
        }

        void Update()
        {
            TargetPosition += velocity * Time.deltaTime;
        }
    }

}