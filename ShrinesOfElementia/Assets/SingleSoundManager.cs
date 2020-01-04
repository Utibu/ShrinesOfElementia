//Author: Niklas Almqvist

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSoundManager : MonoBehaviour
{
    private float currentTime;
    public float WaitTime;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }
    public void PlaySoundIfAvailable(AudioClip clip)
    {
        if (currentTime >= WaitTime && audioSource != null)
        {
            currentTime = 0f;
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("AudioSource is null on " + this.name);
        }
    }

    public void PlaySoundIfAvailable()
    {
        if (currentTime >= WaitTime && audioSource != null)
        {
            currentTime = 0f;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource is null on " + this.name);
        }
    }

    private void Update()
    {
        if (currentTime < WaitTime)
        {
            currentTime += Time.deltaTime;
        }
    }
}