using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Niklas Almqvist

public class GiantAudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource footAudioSource;
    [SerializeField] private AudioSource slashAudioSource;
    [SerializeField] private AudioSource abilityAudioSource;

    [SerializeField] private AudioClip footstepClip;
    [SerializeField] private AudioClip shortFootstepClip;
    [SerializeField] private AudioClip slashClip;
    [SerializeField] private AudioClip chargeAbilityClip;
    [SerializeField] private AudioClip shootAbilityClip;
    [SerializeField] private AudioClip waveClip;

    private float footstepTimer = 0f;
    private float timeBeforeNextAllowedStep = 0.1f;

    public void PlayFootstep()
    {
        footAudioSource.PlayOneShot(footstepClip);
        if (footstepTimer >= timeBeforeNextAllowedStep)
        {
           
            footstepTimer = 0f;
        }
    }

    public void PlayShortFootstep()
    {
        footAudioSource.PlayOneShot(shortFootstepClip);
    }

    public void PlaySlashClip()
    {
        if(slashAudioSource != null)
        {
            slashAudioSource.PlayOneShot(slashClip);
        }
    }

    public void PlayChargeClip()
    {
        if (abilityAudioSource != null)
        {
            abilityAudioSource.PlayOneShot(chargeAbilityClip);
        }
    }

    public void PlayShootClip()
    {
        if (abilityAudioSource != null)
        {
            abilityAudioSource.PlayOneShot(shootAbilityClip);
        }
    }

    public void PlayWaveSound()
    {
        if (abilityAudioSource != null)
        {
            abilityAudioSource.PlayOneShot(waveClip);
        }
    }
}
