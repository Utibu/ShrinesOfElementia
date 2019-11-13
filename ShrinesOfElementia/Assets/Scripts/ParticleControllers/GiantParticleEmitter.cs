// Author: Bilal El Medkouri

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantParticleEmitter : MonoBehaviour
{
    [SerializeField] private ParticleSystem shockwaveParticleSystem;

    public void FireShockwaveParticleSystem()
    {
        shockwaveParticleSystem.Play();
    }
}
