﻿//Author: Joakim Ljung
//co-author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBlade : Ability
{
    //private GameObject[] enemiesHit;
    private List<GameObject> enemiesHit;
    [SerializeField] private int damage;
    [SerializeField] private float lifeTime;
    [SerializeField] private float effectRange; 

    private void Start()
    {
        enemiesHit = new List<GameObject>();
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player")) && !enemiesHit.Contains(other.gameObject) && other.gameObject.tag != caster.tag)
        {
            if (other.gameObject.CompareTag("Shield"))
            {
                Destroy(this.gameObject);
                return;
            }
            EventManager.Current.FireEvent(new DamageEvent("Wind blade dealt " + damage + "to " + other.gameObject, damage, gameObject, other.gameObject));
            EventManager.Current.FireEvent(new WindAbilityEvent("WindAbility activated", other.gameObject, transform.position, effectRange));

            enemiesHit.Add(other.gameObject);
        }
        
    }
}
