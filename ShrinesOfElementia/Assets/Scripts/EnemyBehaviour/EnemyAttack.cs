// Author: Joakim Ljung
// Co-Author: Sofia Chyle Kauko

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //Temporary fix, should get damage from attack state instead
    Collider attackCollider;
    Animator animator;
    [SerializeField] private AudioClip hitSoundClip;
    [SerializeField] private AudioClip blockSoundClip;
    private AudioSource audioSource;

    private void Start()
    {
        attackCollider = GetComponent<Collider>();
        animator = GetComponentInParent<Animator>();
        audioSource = GetComponentInParent<AudioSource>();

        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        //print("Enemy attack collision");

        

        if (collision.collider.gameObject.CompareTag("Shield"))
        {
            gameObject.SetActive(false);
            animator.SetTrigger("AttackBlocked");
            EventManager.Instance.FireEvent(new BlockEvent("Damage blocked: ", 20f));
            if (blockSoundClip != null && Player.Instance.GetComponent<PlayerSoundController>().playerAudioSource != null)
            {
                Player.Instance.GetComponent<PlayerSoundController>().playerAudioSource.PlayOneShot(blockSoundClip, 0.3f);
            }
            print("Player shield hit");
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            //print("Player hit");
            //Debug.Log("::: " + animator.gameObject.GetComponent<EnemyValues>().Damage);
            DamageEvent damageEvent = new DamageEvent(gameObject.name + " did damage to player", (int)animator.gameObject.GetComponent<EnemyValues>().Damage, gameObject, collision.gameObject);
            if(hitSoundClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(hitSoundClip, 0.3f);
            }
            EventManager.Instance.FireEvent(damageEvent);
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
