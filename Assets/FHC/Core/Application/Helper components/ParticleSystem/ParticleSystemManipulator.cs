using UnityEngine;
using System.Collections;

namespace FH.Core.HelperComponent
{
    [RequireComponent(typeof(ParticleSystem))]
    public abstract class ParticleSystemManipulator : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        ParticleSystem targetParticleSystem;
        [SerializeField, ReadOnly]
        SystemState currentState;

        protected enum SystemState { Stop, Play, Pause, Unkown };

        public ParticleSystem TargetParticleSystem
        {
            get
            {
                return targetParticleSystem;
            }
        }

        protected SystemState CurrentState
        {
            get
            {
                return currentState;
            }

            private set
            {
                currentState = value;
            }
        }

        #region MonoB
        public void Awake()
        {
            currentState = DetermineCurrentSystemState(currentState, false);
        }

        public void Update()
        {
            currentState = DetermineCurrentSystemState(currentState, true);
            if (currentState == SystemState.Play)
            {
                UpdateParticleSystem();
            }
        }

        public void Reset()
        {
            targetParticleSystem = GetComponent<ParticleSystem>();
        }
        #endregion

        SystemState DetermineCurrentSystemState(SystemState oldState, bool dispatchEvent)
        {
            if (targetParticleSystem.isStopped)
            {
                if (dispatchEvent && oldState != SystemState.Stop)
                {
                    OnStop();
                }
                return SystemState.Stop;
            }

            if (targetParticleSystem.isPlaying)
            {
                if (dispatchEvent && oldState != SystemState.Play)
                {
                    OnPlay();
                }
                return SystemState.Play;
            }

            if (targetParticleSystem.isPaused)
            {
                if (dispatchEvent && oldState != SystemState.Pause)
                {
                    OnPause();
                }
                return SystemState.Pause;
            }

            return SystemState.Unkown;
        }

        protected virtual void OnStop() { }
        protected virtual void OnPause() {  }
        protected virtual void OnPlay() { }
        protected abstract void UpdateParticleSystem();
    }

}