// Author: Bilal El Medkouri

using UnityEngine;

public class GiantParticleEmitter : MonoBehaviour
{
    [SerializeField] private ParticleSystem shockwaveParticleSystem;
    [SerializeField] private ParticleSystem leapParticleSystem;

    public void FireShockwaveParticleSystem()
    {
        shockwaveParticleSystem.Play();
    }

    public void FireLeapParticleSystem()
    {
        leapParticleSystem.Play();
    }
}
