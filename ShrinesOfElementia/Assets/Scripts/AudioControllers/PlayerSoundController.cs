using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Niklas Almqvist

public class PlayerSoundController : MonoBehaviour
{

    public AudioSource playerAudioSource;
    [SerializeField] private AudioSource footAudioSource;
    [SerializeField] private AudioSource hitAudioSource;

    [SerializeField] private AudioClip slashClip;
    [SerializeField] private AudioClip swordHitClip;
    [SerializeField] private AudioClip enemyHitClip;
    [SerializeField] private AudioClip abilityHitClip;
    [SerializeField] private AudioClip footstepClip;
    [SerializeField] private AudioClip blockClip;
    [SerializeField] private AudioClip[] hurtClip; 

    private float footstepTimer = 0f;
    private float timeBeforeNextAllowedStep = 0.1f;

    public void Start()
    {
        EventManager.Instance.RegisterListener<DamageEvent>(PlayHitClip);
    }
    public void PlaySlashClip()
    {
        playerAudioSource.PlayOneShot(slashClip);
    }

    public void PlayHitClip(DamageEvent ev)
    {
        if(ev.TargetGameObject.tag.Equals("Enemy"))
        {
            if(ev.IsAbility)
            {
                hitAudioSource.PlayOneShot(enemyHitClip);
            } else
            {
                hitAudioSource.PlayOneShot(swordHitClip);

            }
        } else
        {
            if (ev.IsAbility)
            {
                playerAudioSource.PlayOneShot(abilityHitClip);
            }
            else
            {
                playerAudioSource.PlayOneShot(enemyHitClip);

            }
            playerAudioSource.PlayOneShot(hurtClip[Random.Range(0, hurtClip.Length - 1)]);
        }
    }

    public void PlayFootstep()
    {
        if(footstepTimer >= timeBeforeNextAllowedStep)
        {
            footAudioSource.PlayOneShot(footstepClip);
            footstepTimer = 0f;
        }
    }

    public void PlayHurtSound()
    {
        playerAudioSource.PlayOneShot(hurtClip[Random.Range(0, hurtClip.Length - 1)]);
        Debug.LogWarning("Playing clip");
    }

    public void PlayBlockClip()
    {
        playerAudioSource.PlayOneShot(blockClip);
    }

    private void Update()
    {
        footstepTimer += Time.deltaTime;
    }

}
