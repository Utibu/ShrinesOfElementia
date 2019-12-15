using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Niklas Almqvist

public class GiantAudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource footAudioSource;

    [SerializeField] private AudioClip footstepClip;
    [SerializeField] private AudioClip shortFootstepClip;

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
}
