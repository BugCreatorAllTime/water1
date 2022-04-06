using UnityEngine;
using System.Collections;

namespace FH.Core.Gameplay.HelperComponent
{
    public class VerticalBouncing : OutsiteTargetTransform
    {
        [SerializeField]
        float gravityMagnitude;
        [SerializeField]
        float groundY;
        [SerializeField]
        float initialY;

        public float GroundY
        {
            get
            {
                return groundY;
            }

            set
            {
                groundY = value;
            }
        }

        public float GravityMagnitude
        {
            get
            {
                return gravityMagnitude;
            }

            set
            {
                gravityMagnitude = value;
            }
        }

        float currentSpeedY = 0;
        bool inward = true;

        public virtual void Awake()
        {
            ResetState();
        }

        public virtual void Update()
        {
            Vector3 p = TargetPosition;
            float currentY = p.y;

            if (inward)
            {
                currentSpeedY += GravityMagnitude * Time.deltaTime;
                currentY = Mathf.MoveTowards(currentY, groundY, currentSpeedY * Time.deltaTime);
                if (currentY == groundY)
                {
                    inward = false;
                }
            }
            else
            {               
                currentY = Mathf.MoveTowards(currentY, initialY, currentSpeedY * Time.deltaTime);
                currentSpeedY = Mathf.MoveTowards(currentSpeedY, 0, GravityMagnitude * Time.deltaTime);
                if (currentSpeedY == 0)
                {
                    inward = true;
                }
            }

            p.y = currentY;
            TargetPosition = p;
        }

        public void ResetState()
        {
            currentSpeedY = 0;
            inward = true;
            initialY = TargetPosition.y;
        }
    }

}