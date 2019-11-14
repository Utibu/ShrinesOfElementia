// Author: Bilal El Medkouri

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class __LeapParticleCollisionHandler : MonoBehaviour
{
    // Collides twice at the moment, fix this later
    // TODO

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            DamageEvent damageEvent = new DamageEvent(gameObject + " has dealt " + Giant.Instance.LeapDamage + " damage to " + other, Giant.Instance.LeapDamage, gameObject, other);
            EventManager.Current.FireEvent(damageEvent);
        }
    }
}
