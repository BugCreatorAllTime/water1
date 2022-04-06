using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.HelperComponent
{
    public class ParticleConvergence : ParticleManipulator
    {
        [SerializeField]
        PositionProvider convergencePoint;
        [SerializeField]
        float speed;
        Vector3 currentTargetPosition;

        protected override void InitializeNewUpdate()
        {
            currentTargetPosition = convergencePoint.Position;
        }

        protected override ParticleSystem.Particle ManipulateParticle(ParticleSystem.Particle particle)
        {
            particle.position = Vector3.MoveTowards(particle.position, currentTargetPosition, speed * Time.deltaTime);
            return particle;
        }
    }

}