using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{

    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioClip slashClip;
    public void PlaySlashClip()
    {
        playerAudioSource.PlayOneShot(slashClip);
    }

}
