// Author: Niklas Almqvist

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

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
        SetVolume(masterVolume, musicVolume, sfxVolume);
    }

    public void SetVolume(float master, float music, float sfx)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(master) * 20);
        mixer.SetFloat("MusicVolume", Mathf.Log10(music) * 20);
        mixer.SetFloat("SFXVolume", Mathf.Log10(sfx) * 20);
    }

    public void OnUIVolumeChange(float master, float music, float sfx)
    {
        SetVolume(master, music, sfx);
    }

    public void OnMasterVolumeChange(float volume)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void OnMusicVolumeChange(float volume)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void OnSfxVolumeChange(float volume)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }

    private float GetVolume(string volumeType)
    {
        float value;
        bool result = mixer.GetFloat(volumeType, out value);
        return value;
    }

}
