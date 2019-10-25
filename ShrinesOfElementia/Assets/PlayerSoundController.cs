using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{

    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioSource footAudioSource;
    [SerializeField] private AudioClip slashClip;
    [SerializeField] private AudioClip swordHitClip;
    [SerializeField] private AudioClip enemyHitClip;
    [SerializeField] private AudioClip footstepClip;

    public void Start()
    {
        EventSystem.Current.RegisterListener<DamageEvent>(PlayHitClip);
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
        }
    }

    public void PlayFootstep()
    {
        footAudioSource.PlayOneShot(footstepClip);
    }

}
