﻿// Author: Bilal El Medkouri
// Co-Author: Joakim Ljung

using UnityEngine;

public class Geyser : Ability
{
    [Header("Attributes")]
    [SerializeField] private int damage;
    [SerializeField] private float knockUpForce;
    private bool canDealDmg = true;

   

    private void OnTriggerEnter(Collider other)
    {
        if (canDealDmg && (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player")) && other.gameObject.tag != caster.tag)
        {
            DealDamage(other.gameObject, damage);
            canDealDmg = false;

            KnockUp(other);
        }
    }

    private void DealDamage(GameObject damagedGameObject, int damage)
    {
        DamageEvent damageEvent = new DamageEvent(damagedGameObject + " has dealt " + damage + " damage to " + damagedGameObject, damage, gameObject, damagedGameObject);
        EventManager.Instance.FireEvent(damageEvent);
    }

   

    private void KnockUp(Collider colliderHit)
    {
        // Put this on hold for now
    }
    /*
    private void SelfDestruct()
    {
        print("Self Destruct");
        Destroy(gameObject);
    }
    */
}
