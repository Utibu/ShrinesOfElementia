// Author: Niklas Almqvist

using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioMixer mixer;
    /*
    [SerializeField] private AudioMixerGroup masterGroup;
    [SerializeField] private AudioMixerGroup musicGroup;
    [SerializeField] private AudioMixerGroup sfxGroup;
    */


    /*
     * IMPORTANT!
     * The sliders need to go from 0.001f to 1f or something similar.
    */

    public float masterVolume = 0f;
    public float musicVolume = 1f;
    public float sfxVolume = 1f;

    private void Awake()
    {
        // Prevents multiple instances
        if (Instance == null) { Instance = this; }
        else { Debug.Log("Warning: multiple " + this + " in scene!"); }

    }

    private void Start()
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
        mixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
    }


}
