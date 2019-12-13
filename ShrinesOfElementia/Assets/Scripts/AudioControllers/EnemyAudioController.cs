using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip spinClip;
    [SerializeField] private AudioClip dieClip;
    private AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1f;
    }

    public void PlaySpinClip()
    {
        this.GetComponent<AudioSource>().PlayOneShot(spinClip);
    }

    public void PlayDieClip()
    {
        Debug.Log("PLAY DIE CLIP");
        if(dieClip != null && audioSource != null)
        {
            this.audioSource.PlayOneShot(dieClip);
        }
    }
}
