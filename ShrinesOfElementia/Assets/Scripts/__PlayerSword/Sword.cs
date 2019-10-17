// Author: Bilal El Medkouri

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private int damage;
    private Vector3 hitPoint;

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Enemy"))
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
            {
            print("Enemy/player hit");

            hitPoint = collision.GetContact(0).point;

            DamageEvent damageEvent = new DamageEvent(damage, this.gameObject, collision.gameObject);
            damageEvent.FireEvent();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(hitPoint, .2f);
    }
}
