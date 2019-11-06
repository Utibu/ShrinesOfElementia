using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpikes : MonoBehaviour
{
    private ParticleSystem particles;

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
    }

    private void OnParticleCollision(GameObject other)
    {
        print("particle collision");
        print(other.gameObject.name);
        if (other.CompareTag("Enemy"))
        {
            print("enemy hit");
        }
    }
}
