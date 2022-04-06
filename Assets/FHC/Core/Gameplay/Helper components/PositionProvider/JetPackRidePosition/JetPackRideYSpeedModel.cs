using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Gameplay.HelperComponent
{
    [System.Serializable]
    public class JetPackRideYSpeedModel : IJetPackRideYSpeedModel
    {
        [Header("Fall")]
        [SerializeField]
        float initialFallSpeed = -2;
        [SerializeField]
        float fallAcceleration = -2;

        [Header("Rise")]
        [SerializeField]
        float initialRiseSpeed = 2;
        [SerializeField]
        float riseAcceleration = 2;

        [SerializeField, ReadOnly]
        float speed;

        #region IJetPackRideYSpeedModel
        public float CurrentSpeed
        {
            get
            {
                return speed;
            }
        }

        public void OnHitCeil()
        {
            speed = 0;
        }

        public void OnHitGround()
        {
            speed = 0;
        }

        public void OnInitialize()
        {
            speed = 0;
        }

        public void OnStartFall()
        {
            speed = initialFallSpeed;
        }

        public void OnStartRise()
        {
            speed = initialRiseSpeed;
        }

        public void UpdateFall()
        {
            speed += fallAcceleration * Time.deltaTime;
        }

        public void UpdateRise()
        {
            speed += riseAcceleration * Time.deltaTime;
        }
        #endregion
    }

}