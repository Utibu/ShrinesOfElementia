// Author: Niklas Almqvist

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private TextMeshProUGUI masterLabel;
    [SerializeField] private TextMeshProUGUI musicLabel;
    [SerializeField] private TextMeshProUGUI sfxLabel;

    /*
    [SerializeField] private AudioMixerGroup masterGroup;
    [SerializeField] private AudioMixerGroup musicGroup;
    [SerializeField] private AudioMixerGroup sfxGroup;
    */


    /*
     * IMPORTANT!
     * The sliders need to go from 0.001f to 1f or something similar.
    */

    public float masterVolume = 0.5f;
    public float musicVolume = 1f;
    public float sfxVolume = 1f;

    private bool masterMuted;
    private bool musicMuted;
    private bool sfxMuted;

    private void Awake()
    {
        // Prevents multiple instances
        if (Instance == null) { Instance = this; }
        else { Debug.Log("Warning: multiple " + this + " in scene!"); }

        

    }

    private void Start()
    {

        if (PlayerPrefs.GetInt("HasRunVolume") != 1)
        {
            PlayerPrefs.SetInt("HasRunVolume", 1);
            //OnUIVolumeChange(masterVolume, musicVolume, sfxVolume);
            OnMasterVolumeChange(masterVolume);
            OnMusicVolumeChange(musicVolume);
            OnSfxVolumeChange(sfxVolume);

            //PlayerPrefs.SetInt("MasterIsMuted", 0);
            //PlayerPrefs.SetInt("MusicIsMuted", 0);
            //PlayerPrefs.SetInt("SFXIsMuted", 0);
        }
        else
        {
            SetVolume(PlayerPrefs.GetFloat("MasterVolumeRaw"), PlayerPrefs.GetFloat("MusicVolumeRaw"), PlayerPrefs.GetFloat("SFXVolumeRaw"));
        }

        Debug.Log("mvr: " + PlayerPrefs.GetFloat("MusicVolumeRaw"));

        if (masterSlider != null)
        {
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolumeRaw");
        }

        if (musicSlider != null)
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolumeRaw");
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolumeRaw");
        }
    }

    public void SetVolume(float master, float music, float sfx)
    {
        Debug.Log("SETVOLUME!");
        mixer.SetFloat("MasterVolume", Mathf.Log10(master) * 20);
        mixer.SetFloat("MusicVolume", Mathf.Log10(music) * 20);
        mixer.SetFloat("SFXVolume", Mathf.Log10(sfx) * 20);
        //OnMasterVolumeChange(master);
        //OnMusicVolumeChange(music);
        //OnSfxVolumeChange(sfx);
    }

    public void OnUIVolumeChange(float master, float music, float sfx)
    {
        SetVolume(master, music, sfx);
    }

    public void OnMasterVolumeChange(float volume)
    {
        float newVolumeDb = Mathf.Log10(volume) * 20;
        mixer.SetFloat("MasterVolume", newVolumeDb);
        PlayerPrefs.SetFloat("MasterVolumeRaw", volume);

        if(masterLabel != null)
        {
            masterLabel.text = "" + Mathf.FloorToInt(volume * 100);
        }
      //  PlayerPrefs.Save();
    }

    public void OnMusicVolumeChange(float volume)
    {
        float newVolumeDb = Mathf.Log10(volume) * 20;
        mixer.SetFloat("MusicVolume", newVolumeDb);
        PlayerPrefs.SetFloat("MusicVolumeRaw", volume);

        if (musicLabel != null)
        {
            musicLabel.text = "" + Mathf.FloorToInt(volume * 100);
        }
        // PlayerPrefs.Save();
    }

    public void OnSfxVolumeChange(float volume)
    {
        float newVolumeDb = Mathf.Log10(volume) * 20;
        mixer.SetFloat("SFXVolume", newVolumeDb);
        PlayerPrefs.SetFloat("SFXVolumeRaw", volume);

        if (sfxLabel != null)
        {
            sfxLabel.text = "" + Mathf.FloorToInt(volume * 100);
        }
        //PlayerPrefs.Save();
    }

    /*public void SetMasterMute(Toggle toggle)
    {
        if(toggle.isOn)
        {
            OnMasterVolumeChange(0.001f);

        } else
        {
            //Mute
        }
    }*/

    private float GetVolume(string volumeType)
    {
        float value;
        bool result = mixer.GetFloat(volumeType, out value);
        return value;
    }

}
