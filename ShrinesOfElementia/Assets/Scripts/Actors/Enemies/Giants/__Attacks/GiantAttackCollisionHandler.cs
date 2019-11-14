// Author: Bilal El Medkouri

using UnityEngine;

public class GiantAttackCollisionHandler : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DamageEvent damageEvent = new DamageEvent(gameObject + " has dealt " + damage + " damage to " + collision.gameObject, damage, gameObject, collision.gameObject, "Melee");
            EventManager.Current.FireEvent(damageEvent);
        }
    }
}
