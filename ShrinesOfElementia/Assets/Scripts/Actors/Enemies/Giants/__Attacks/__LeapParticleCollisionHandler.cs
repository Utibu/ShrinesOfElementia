// Author: Bilal El Medkouri

using UnityEngine;

public class __LeapParticleCollisionHandler : MonoBehaviour
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
        if (other.CompareTag("Player"))
        {
            DamageEvent damageEvent = new DamageEvent(gameObject + " has dealt " + Giant.Instance.LeapDamage + " damage to " + other, Giant.Instance.LeapDamage, gameObject, other);
            EventManager.Instance.FireEvent(damageEvent);

            hits += 2;
        }
    }
}
