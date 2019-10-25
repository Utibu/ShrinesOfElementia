using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //Temporary fix, should get damage from attack state instead
    private float damage;
    Collider attackCollider;
    Animator animator;

    private void Start()
    {
        damage = 20;
        attackCollider = GetComponent<Collider>();
        animator = GetComponentInParent<Animator>();
        print(attackCollider.gameObject.name);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        print("Enemy attack collision");


        if (collision.collider.gameObject.CompareTag("Shield"))
        {
            attackCollider.gameObject.SetActive(false);
            animator.SetTrigger("AttackBlocked");
            print("Player shield hit");
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            attackCollider.gameObject.SetActive(false);
            print("Player hit");
            DamageEvent damageEvent = new DamageEvent(gameObject.name + " did " + damage + " to player", (int)damage, gameObject, collision.gameObject);
            EventSystem.Current.FireEvent(damageEvent);
        }
    }
    

    /*
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
    */
}
