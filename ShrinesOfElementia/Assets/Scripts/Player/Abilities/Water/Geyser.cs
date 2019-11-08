// Author: Bilal El Medkouri

using UnityEngine;

public class Geyser : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int damage;
    [SerializeField] private float knockUpForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            DealDamage(other.gameObject, damage);

            KnockUp(other);
        }
    }

    private void DealDamage(GameObject damagedGameObject, int damage)
    {
        DamageEvent damageEvent = new DamageEvent(damagedGameObject + " has dealt " + damage + " damage to " + damagedGameObject, damage, gameObject, damagedGameObject);
        EventSystem.Current.FireEvent(damageEvent);
    }

    private void KnockUp(Collider colliderHit)
    {
        // Put this on hold for now
    }

    private void SelfDestruct()
    {
        print("Self Destruct");
        Destroy(gameObject);
    }
}
