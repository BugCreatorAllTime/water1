using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ColorViewParticles : ColorView
{
    protected override void SetColor(Color color)
    {
        var particleSystem = GetComponent<ParticleSystem>();
        var setting = particleSystem.main;
        setting.startColor = color;
    }
}
