using UnityEngine;
using System.Collections;
using FH.Core.Architecture;

namespace FH.Core.Gameplay.HelperComponent
{
    public class FreeFall : OutsiteTargetTransform
    {
        [SerializeField]
        PositionProvider destination;
        [SerializeField]
        float gravity = 25;
        [SerializeField]
        float initialSpeed;

        [Space]
        [SerializeField]
        OrderedEventDispatcher stopFallingEvent = new OrderedEventDispatcher();

        float currentSpeed;

        bool falling = false;

        public float Gravity
        {
            get
            {
                return gravity;
            }

            set
            {
                gravity = value;
            }
        }

        void OnEnable()
        {
            if (!falling)
            {
                enabled = falling;
            }
        }

        public void Update()
        {
            if (MoveTargetTransform() == destination.Position)
            {
                StopFalling();
                return;
            }

            currentSpeed += Gravity * Time.deltaTime;


        }

        Vector3 MoveTargetTransform()
        {
            Vector3 position = TargetPosition;
            position = Vector3.MoveTowards(position, destination.Position, currentSpeed * Time.deltaTime);
            TargetPosition = position;
            return position;
        }

        [ContextMenu("Start fall")]
        public void StartFall()
        {
            currentSpeed = initialSpeed;
            falling = true;
            enabled = true;
        }

        void StopFalling()
        {
            falling = false;
            enabled = falling;
            stopFallingEvent.Dispatch();
        }
    }

}