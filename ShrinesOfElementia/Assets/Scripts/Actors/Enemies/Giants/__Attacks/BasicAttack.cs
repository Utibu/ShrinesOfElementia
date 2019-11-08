// Author: Bilal El Medkouri

using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnCollisionEnter(Collision collision)
    {
        print("Giant Basic Attack hit");

        if (collision.gameObject.CompareTag("Player"))
        {
            DamageEvent damageEvent = new DamageEvent(gameObject + " has dealt " + damage + " damage to " + collision.gameObject, damage, gameObject, collision.gameObject);
            EventSystem.Current.FireEvent(damageEvent);
        }
    }
}
