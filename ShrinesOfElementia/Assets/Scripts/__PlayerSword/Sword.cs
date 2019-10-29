// Author: Bilal El Medkouri

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private GameObject hitParticleSystem;
    private Vector3 hitPoint;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hitPoint = collision.GetContact(0).point;

            if(collision.gameObject.CompareTag("Enemy"))
            {
                GameObject go = (GameObject)Instantiate(hitParticleSystem, hitPoint, Quaternion.identity);
                Destroy(go, 1f);
            }

            DamageEvent damageEvent = new DamageEvent(gameObject + " has dealt " + damage + " damage to " + collision.gameObject, damage, gameObject, collision.gameObject);
            EventSystem.Current.FireEvent(damageEvent);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(hitPoint, .2f);
    }
}
