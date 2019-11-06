using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip spinClip;

    public void PlaySpinClip()
    {
        this.GetComponent<AudioSource>().PlayOneShot(spinClip);
    }
}
