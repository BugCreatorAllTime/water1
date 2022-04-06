using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Gameplay.HelperComponent
{
    public class JetPackRidePositionProvider : PositionProvider
    {
        [SerializeField]
        PositionProvider referencePositionProvider;

        [SerializeField]
        float maxY;
        [SerializeField]
        float minY;

        public enum State
        {
            Fall,
            Rise
        }
        [SerializeField]
        State initialState = State.Rise;
        [SerializeField, ReadOnly]
        State state = State.Fall;
        [SerializeField, ReadOnly]
        Vector3 position;
        [SerializeField]
        JetPackRideYSpeedModel ySpeedModel = new JetPackRideYSpeedModel();

        int lastUpdatedFrame = 0;

        public override Vector3 Position
        {
            get
            {
                if (lastUpdatedFrame != Time.frameCount)
                {
                    UpdatePosition();
                    lastUpdatedFrame = Time.frameCount;
                }

                return position;
            }
        }

        public void UpdatePosition()
        {
            Vector3 referencePosition = referencePositionProvider.Position;
            position.z = referencePosition.z;
            position.x = referencePosition.x;

            position.y += ySpeedModel.CurrentSpeed * Time.deltaTime;
            position.y = Mathf.Clamp(position.y, minY, maxY);

            if (position.y == minY)
            {
                ySpeedModel.OnHitGround();
            }
            else
            {
                if (position.y == maxY)
                {
                    ySpeedModel.OnHitCeil();
                }
            }

            switch (state)
            {
                case State.Fall:
                    ySpeedModel.UpdateFall();
                    break;
                case State.Rise:
                    ySpeedModel.UpdateRise();
                    break;
                default:
                    break;
            }

        }

        public void Initialize()
        {
            ySpeedModel.OnInitialize();
            position = referencePositionProvider.Position;
            state = initialState;
            if (state == State.Fall)
            {
                ySpeedModel.OnStartFall();
            }
            else
            {
                ySpeedModel.OnStartRise();
            }
        }

        public void SwitchToFall()
        {
            state = State.Fall;
            ySpeedModel.OnStartFall();
        }

        public void SwitchToRise()
        {
            state = State.Rise;
            ySpeedModel.OnStartRise();
        }
    }

}