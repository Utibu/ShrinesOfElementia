// Author: Bilal El Medkouri

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantAttackParticleCollisionHandler : MonoBehaviour
{
    // Collides twice at the moment, fix this later
    // TODO

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            DamageEvent damageEvent = new DamageEvent(gameObject + " has dealt " + Giant.Instance.StompDamage + " damage to " + other, Giant.Instance.StompDamage, gameObject, other);
            EventManager.Current.FireEvent(damageEvent);
        }
    }
}
