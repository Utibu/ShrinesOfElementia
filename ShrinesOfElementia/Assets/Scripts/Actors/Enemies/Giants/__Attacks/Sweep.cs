// Author: Bilal El Medkouri

using UnityEngine;

public class Sweep : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnCollisionEnter(Collision collision)
    {
        print("Sweep hit");

        if (collision.gameObject.CompareTag("Player"))
        {
            DamageEvent damageEvent = new DamageEvent(gameObject + " has dealt " + damage + " damage to " + collision.gameObject, damage, gameObject, collision.gameObject);
            EventManager.Current.FireEvent(damageEvent);
        }
    }
}
