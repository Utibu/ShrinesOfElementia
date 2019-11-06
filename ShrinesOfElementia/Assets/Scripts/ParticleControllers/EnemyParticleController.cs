using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;

    public void EnableParticleSystem()
    {
        particles.Play();
    }

    public void StopParticleSystem()
    {
        particles.Stop();
    }
}
