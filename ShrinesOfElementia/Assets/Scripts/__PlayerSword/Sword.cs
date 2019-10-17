// Author: Bilal El Medkouri

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Vector3 hitPoint;


    private void OnTriggerEnter(Collider other)
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position + (transform.forward * 1f), -transform.forward, out hit, 8, layerMask))
        {
            print("hit");

            hitPoint = hit.point;

            // Deal damage
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Ray ray = new Ray(transform.position + (transform.forward * 1f), -transform.forward);
        Gizmos.DrawRay(ray);

        Gizmos.DrawSphere(hitPoint, .2f);
    }
}
