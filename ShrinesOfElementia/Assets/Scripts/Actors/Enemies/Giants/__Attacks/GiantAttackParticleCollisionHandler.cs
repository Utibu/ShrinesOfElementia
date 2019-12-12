// Author: Bilal El Medkouri

using UnityEngine;

public class GiantAttackParticleCollisionHandler : MonoBehaviour
{
    private int hits = 0;


    private void LateUpdate()
    {
        if (hits > 0)
        {
            hits--;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player") && hits == 0)
        {
            DamageEvent damageEvent = new DamageEvent(gameObject + " has dealt " + Giant.Instance.StompDamage + " damage to " + other, Giant.Instance.StompDamage, gameObject, other);
            EventManager.Instance.FireEvent(damageEvent);

            hits++;
        }
    }
}
