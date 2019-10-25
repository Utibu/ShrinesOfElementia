using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //Temporary fix, should get damage from attack state instead
    private float damage;
    BoxCollider attackCollider;
    Animator animator;

    private void Start()
    {
        damage = 20;
        attackCollider = GetComponent<BoxCollider>();
        animator = GetComponentInParent<Animator>();
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        print(collision.collider.gameObject.name);
        
        if (collision.collider.gameObject.CompareTag("Shield"))
        {
            attackCollider.enabled = false;
            print("Player shield hit");
        }
        else if (collision.collider.gameObject.CompareTag("Player"))
        {
            print("Player hit");
            DamageEvent damageEvent = new DamageEvent(gameObject.name + " did " + damage + " to player", (int)damage, gameObject, collision.gameObject);
            EventSystem.Current.FireEvent(damageEvent);
        }
    }
    */


    private void OnTriggerEnter(Collider other)
    {
        attackCollider.gameObject.SetActive(false);
        if (other.gameObject.tag == "Shield")
        {
            animator.SetTrigger("AttackBlocked");
            print("Player shield hit");
        }
        else if (other.gameObject.tag == "Player")
        {
            print("Player hit");
            DamageEvent damageEvent = new DamageEvent(gameObject.name + " did " + damage + " to player", (int)damage, gameObject, other.gameObject);
            EventSystem.Current.FireEvent(damageEvent);
        }
    }
}
