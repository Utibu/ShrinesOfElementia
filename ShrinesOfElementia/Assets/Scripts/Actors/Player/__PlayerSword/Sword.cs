// Author: Bilal El Medkouri
//co-Author Sofia Kauko

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int getDamage { get { return damage; } }

    [SerializeField] private int damage;
    [SerializeField] private GameObject hitParticleSystem;
    private Vector3 hitPoint;


    /*  Attack detection with collider (no trigger)
    private void OnCollisionEnter(Collision collision)
    {
        print("hit");

        if (collision.gameObject.CompareTag("Enemy")) //Should fix later. Attacks should be normalized
        {
            hitPoint = collision.GetContact(0).point;

            if(collision.gameObject.CompareTag("Enemy"))
            {
                GameObject go = (GameObject)Instantiate(hitParticleSystem, hitPoint, Quaternion.identity);
                Destroy(go, 1f);
            }

            DamageEvent damageEvent = new DamageEvent(gameObject + " has dealt " + damage + " damage to " + collision.gameObject, damage, gameObject, collision.gameObject);
            EventManager.Current.FireEvent(damageEvent);
        }
    }
    */

    //Attack detection with trigger
    private void OnTriggerEnter(Collider other)
    {
        print("hit");

        if (other.gameObject.CompareTag("Enemy")) //Should fix later. Attacks should be normalized
        {
            hitPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position); //used to find impact point
            GameObject go = (GameObject)Instantiate(hitParticleSystem, hitPoint, Quaternion.identity);
            Destroy(go, 1f);

            DamageEvent damageEvent = new DamageEvent(gameObject + " has dealt " + damage + " damage to " + other.gameObject, damage, gameObject, other.gameObject);
            EventManager.Instance.FireEvent(damageEvent);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(hitPoint, .2f);
    }

    public void SetDamage(int damage)
    {
        this.damage += damage;
    }


    public void SetActive()
    {
        GetComponent<Collider>().enabled = true;
    }

    public void SetDisabled()
    {
        GetComponent<Collider>().enabled = false;
    }
}
