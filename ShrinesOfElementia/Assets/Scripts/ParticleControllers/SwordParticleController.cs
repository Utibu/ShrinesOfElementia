using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;

    public void StartParticles()
    {
        particles.Play();
    }

    public void StopParticles()
    {
        particles.Stop();
    }
}
