﻿//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalWeakness : MonoBehaviour
{
    [SerializeField] private int elementalAdvantageDamageBonus;


    protected void ReactToElement()
    {
        //if enemy, stop particles and disable casting, maybe even slow down? 
        EnemySM enemy;
        if (gameObject.TryGetComponent<EnemySM>(out enemy))
        {
            enemy.DisableEnemy();
        }

        

        //deal that extra elemental damage to anything w a healthcomponent
        HealthComponent health;
        if (gameObject.TryGetComponent<HealthComponent>(out health))
        {
            health.CurrentHealth -= elementalAdvantageDamageBonus;
        }
    }


}
