//Author: Niklas Almqvist
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip spinClip;
    [SerializeField] private AudioClip dieClip;
    [SerializeField] private AudioClip castClip;
    [SerializeField] private AudioClip meleeWindupClip;
    private AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1f;
    }

    public void PlaySpinClip()
    {
        if (spinClip != null && audioSource != null) 
        {
            this.GetComponent<AudioSource>().PlayOneShot(spinClip);
        }
    }

    public void PlayDieClip()
    {
        Debug.Log("PLAY DIE CLIP");
        if(dieClip != null && audioSource != null)
        {
            this.audioSource.PlayOneShot(dieClip);
        }
    }

    public void PlayCastSound()
    {
        if (castClip != null && audioSource != null)
        {
            this.audioSource.PlayOneShot(castClip);
        }
    }

    public void PlayMeleeWindup()
    {
        if (meleeWindupClip != null && audioSource != null)
        {
            this.audioSource.PlayOneShot(meleeWindupClip);
        }
    }
}
