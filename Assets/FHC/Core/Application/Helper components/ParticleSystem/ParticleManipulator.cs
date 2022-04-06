using UnityEngine;
using System.Collections;

namespace FH.Core.HelperComponent
{
    [RequireComponent(typeof(ParticleSystem))]   
    public abstract class ParticleManipulator : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        ParticleSystem targetParticleSystem;

        [SerializeField]
        ParticleSystem.Particle[] particles;

        protected ParticleSystem TargetParticleSystem
        {
            get
            {
                return targetParticleSystem;
            }
        }

        public void Update()
        {
            ReinitializeIfNeeded();
            InitializeNewUpdate();
            int numberOfAliveParticles = targetParticleSystem.GetParticles(particles);
            for (int i = 0; i < numberOfAliveParticles; i++)
            {
                particles[i] = ManipulateParticle(particles[i]);
            }
            targetParticleSystem.SetParticles(particles, numberOfAliveParticles);
        }

        protected virtual void InitializeNewUpdate() { }
        protected abstract ParticleSystem.Particle ManipulateParticle(ParticleSystem.Particle particle);

        void ReinitializeIfNeeded()
        {
            if (particles == null || particles.Length < targetParticleSystem.maxParticles)
            {
                particles = new ParticleSystem.Particle[targetParticleSystem.maxParticles];
            }
        }

        public void OnValidate()
        {
            particles = new ParticleSystem.Particle[targetParticleSystem.maxParticles];
        }

        public void Reset()
        {
            targetParticleSystem = GetComponent<ParticleSystem>();
            targetParticleSystem.maxParticles = 100;
        }
    }

}