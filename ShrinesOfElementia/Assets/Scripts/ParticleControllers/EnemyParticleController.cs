
//co-Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    
    

    public void EnableParticleSystem()
    {
        foreach (ParticleSystem system in GetComponentsInChildren<ParticleSystem>())
        {
            if (!system.Equals(particles))
            {
                system.Play();
            }
        }
          
    }

    public void StopParticleSystem()
    {
        foreach (ParticleSystem system in GetComponentsInChildren<ParticleSystem>())
        {
            if (!system.Equals(particles))
            {
                system.Stop();
            }
        }
    }

    public void EnableAttackParticles()
    {
        particles.Play();
    }

    public void DisableAttackParticles()
    {
        particles.Stop();
        
    }

}
