// Author: Bilal El Medkouri

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Vector3 hitPoint;

    private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Hit " + collision.gameObject);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            print("Enemy hit");

            hitPoint = collision.GetContact(0).point;

            // Deal damage
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(hitPoint, .2f);
    }
}
