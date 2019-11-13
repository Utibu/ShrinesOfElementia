// Author: Bilal El Medkouri
// Co-Author: Joakim Ljung

using UnityEngine;

public class Geyser : Ability
{
    [Header("Attributes")]
    [SerializeField] private int damage;
    [SerializeField] private float knockUpForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            DealDamage(other.gameObject, damage);

            KnockUp(other);
        }
    }

    private void DealDamage(GameObject damagedGameObject, int damage)
    {
        DamageEvent damageEvent = new DamageEvent(damagedGameObject + " has dealt " + damage + " damage to " + damagedGameObject, damage, gameObject, damagedGameObject);
        EventManager.Current.FireEvent(damageEvent);
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
