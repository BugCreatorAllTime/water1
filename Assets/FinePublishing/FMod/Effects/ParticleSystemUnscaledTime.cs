using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemUnscaledTime : MonoBehaviour
{
    new ParticleSystem particleSystem;

    bool isStimulating = false;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (Time.timeScale < 0.01f)
        {
            isStimulating = true;
            particleSystem.Simulate(Time.unscaledDeltaTime, true, false);
        }
        else
        {
            if (isStimulating)
            {
                isStimulating = false;
                particleSystem.Play();
            }
        }
    }
}
