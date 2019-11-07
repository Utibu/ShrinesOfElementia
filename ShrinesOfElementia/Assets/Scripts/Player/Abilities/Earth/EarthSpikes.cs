﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpikes : MonoBehaviour
{
    private ParticleSystem particles;
    private BoxCollider boxCollider;

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (particles.IsAlive())
        {
            boxCollider.enabled = true;
        }
        else
        {
            boxCollider.enabled = false;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            print("trigger particle");
            EventSystem.Current.FireEvent(new DamageEvent("damage dealt", 20, gameObject, other.gameObject));
        }
    }
}
