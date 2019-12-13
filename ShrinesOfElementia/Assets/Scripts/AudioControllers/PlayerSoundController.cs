using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{

    public AudioSource playerAudioSource;
    [SerializeField] private AudioSource footAudioSource;
    [SerializeField] private AudioClip slashClip;
    [SerializeField] private AudioClip swordHitClip;
    [SerializeField] private AudioClip enemyHitClip;
    [SerializeField] private AudioClip footstepClip;
    [SerializeField] private AudioClip parryClip;
    [SerializeField] private AudioClip blockClip;
    [SerializeField] private AudioClip hurtClip;

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
            playerAudioSource.PlayOneShot(swordHitClip);
        } else
        {
            playerAudioSource.PlayOneShot(enemyHitClip);
            playerAudioSource.PlayOneShot(hurtClip);
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
        playerAudioSource.PlayOneShot(hurtClip);
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
