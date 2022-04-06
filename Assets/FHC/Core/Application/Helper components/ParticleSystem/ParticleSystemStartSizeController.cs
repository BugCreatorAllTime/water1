using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.HelperComponent
{
    public class ParticleSystemStartSizeController : ParticleSystemManipulator
    {
        [SerializeField]
        float minStartSize = 0;
        [SerializeField]
        float maxStartSize = 1;
        [SerializeField]
        float lifeTimeToReachMax = 0.4f;

        protected override void UpdateParticleSystem()
        {
            TargetParticleSystem.startSize = Mathf.Lerp(minStartSize, maxStartSize, TargetParticleSystem.CurrentProgressToSpecificLifeTime(lifeTimeToReachMax));
        }
    }

}