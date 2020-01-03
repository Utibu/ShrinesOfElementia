using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Niklas Almqvist

public class PlayerSoundController : MonoBehaviour
{

    public AudioSource playerAudioSource;
    [SerializeField] private AudioSource footAudioSource;
    [SerializeField] private AudioSource hitAudioSource;
    [SerializeField] private AudioSource channelingAudioSource;
    [SerializeField] private AudioSource healthOrbsAudioSource;
    [SerializeField] private AudioSource jingleAudioSource;

    [SerializeField] private AudioClip slashClip;
    [SerializeField] private AudioClip swordHitClip;
    [SerializeField] private AudioClip enemyHitClip;
    [SerializeField] private AudioClip abilityHitClip;
    [SerializeField] private AudioClip waterHitClip;
    [SerializeField] private AudioClip abilityHitEnemyClip;
    [SerializeField] private AudioClip footstepClip;
    [SerializeField] private AudioClip shrineTakenClip;
    [SerializeField] private AudioClip shrineChannelingClip;
    [SerializeField] private AudioClip blockClip;
    [SerializeField] private AudioClip jingleClip;
    [SerializeField] private AudioClip healthOrbClip;
    [SerializeField] private AudioClip[] hurtClip; 

    private float footstepTimer = 0f;
    private float timeBeforeNextAllowedStep = 0.1f;

    private float damageTakenTimer = 0f;
    private float timeBeforeDamageAllowedToPlay = 0.1f;

    public void Start()
    {
        EventManager.Instance.RegisterListener<DamageEvent>(PlayHitClip);
        EventManager.Instance.RegisterListener<LevelUpEvent>(PlayLevelUpJingle);
    }
    public void PlaySlashClip()
    {
        playerAudioSource.PlayOneShot(slashClip);
    }

    public void PlayLevelUpJingle(LevelUpEvent ev)
    {
        jingleAudioSource.PlayOneShot(jingleClip);
    }

    public void PlayOrbClip()
    {
        healthOrbsAudioSource.PlayOneShot(healthOrbClip);
    }


    public void PlayHitClip(DamageEvent ev)
    {
        if(ev.TargetGameObject.tag.Equals("Enemy"))
        {
            if(ev.IsAbility)
            {
                playerAudioSource.PlayOneShot(abilityHitEnemyClip);
            } else
            {
                hitAudioSource.PlayOneShot(swordHitClip);

            }
        } else
        {

            if (damageTakenTimer >= timeBeforeDamageAllowedToPlay)
            {
                if(ev.InflictedFromWater && waterHitClip != null)
                {
                    Debug.LogWarning("WAterhitclip?");
                    playerAudioSource.PlayOneShot(waterHitClip);
                }
                else if (ev.IsAbility)
                {
                    playerAudioSource.PlayOneShot(abilityHitClip);
                }
                else
                {
                    playerAudioSource.PlayOneShot(enemyHitClip);

                }
                playerAudioSource.PlayOneShot(hurtClip[Random.Range(0, hurtClip.Length - 1)]);
                damageTakenTimer = 0f;
            }
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
        damageTakenTimer += Time.deltaTime;
    }

    public void PlayChannelingClip()
    {
        channelingAudioSource.PlayOneShot(shrineChannelingClip);
    }

    public void StopChannelingClip()
    {
        channelingAudioSource.Stop();
    }

    public void PlayShrineTakenClip()
    {
        hitAudioSource.PlayOneShot(shrineTakenClip);
    }

}
