// Author: Bilal El Medkouri

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the fireball prefab
/// </summary>
public class Fireball : MonoBehaviour
{
    [Header("Temporary attributes")]
    [SerializeField] private int damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DamageEvent damageEvent = new DamageEvent(gameObject + " has dealt " + damage + " damage to " + collision.gameObject, damage, gameObject, collision.gameObject);
            EventSystem.Current.FireEvent(damageEvent);
        }

        // Add AoE-effect here

        Destroy(gameObject);
    }
}