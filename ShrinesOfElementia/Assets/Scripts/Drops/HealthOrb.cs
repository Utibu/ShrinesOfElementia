﻿//Author: Joakim Ljung

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb : DroppableObject
{
    [SerializeField] int healthAmount;

    protected override void Start()
    {
        healthAmount = Player.Instance.healthDropsAmount;
        /*if(BalancingManager.Instance.hasSetupHealthOrbs == false)
        {
            BalancingManager.Instance.SetHealthOrbsValue(healthAmount);
            BalancingManager.Instance.hasSetupHealthOrbs = true;
        } else
        {
            healthAmount = (int)BalancingManager.Instance.Healthdrops;
        }*/
    }

    protected override void Update()
    {
        
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        print("Collision");
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Collision with player");
            Player.Instance.Health.CurrentHealth += healthAmount;
            Destroy(gameObject);
        }

    }
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.Health.CurrentHealth += healthAmount;
            Player.Instance.GetComponent<PlayerSoundController>().PlayOrbClip();
            Destroy(droppedObject);
        }
    }
    
}
