//Author: Joakim Ljung

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class OptionsManager : MonoBehaviour
{

    Resolution[] resolutions;
    //[SerializeField] private Dropdown resolutionDropdown;
    [SerializeField]private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown antiAliasingDropdown;
    [SerializeField] private TMP_Dropdown screenModeDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;

    List<Resolution> allowedResolutions;

    private void Start()
    {
        LoadSettings();
    }

    public void LoadSettings()
    {
        resolutions = Screen.resolutions.Where(x => x.width <= 2560).Distinct().OrderBy(x => x.width)
            .ThenBy(x => x.height).ThenBy(x => x.refreshRate).ToArray();
        List<string> options = new List<string>();
        allowedResolutions = new List<Resolution>();
        allowedResolutions.Clear();
        int currentResolutionIndex = 0;
        int currentWidth = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {

            string option = resolutions[i].width + " x " + resolutions[i].height + " - " + resolutions[i].refreshRate + "Hz";
            
            float aspectRatio = (float)resolutions[i].width / (float)resolutions[i].height;
            float targetRatio = 1920f / 1080f;
           // Debug.Log("WIDTH: " + resolutions[i].width + " HEIGHT: " + resolutions[i].height);
           // Debug.Log("ASPECT1: " + aspectRatio);
            //Debug.Log("TARGET: " + targetRatio);
            //if (options.Exists(x => x == option) == false && (aspectRatio == targetRatio)) {
            //if(resolutions[i].width <= 2560)
            //{
                options.Add(option);
                //allowedResolutions.Add(resolutions[i]);
                //Debug.LogError("CURRENT RES: " + Screen.currentResolution.width);
                if (resolutions[i].width + currentWidth == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {

                // currentResolutionIndex = allowedResolutions.IndexOf(resolutions[i]);
                currentResolutionIndex = i;
                }
           // }
                
            //}
            
        }

        if(Screen.currentResolution.width > 2560)
        {
            Screen.SetResolution(1920, 1080, Screen.fullScreen);
        }
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();


        switch (Screen.fullScreenMode)
        {
            case FullScreenMode.Windowed:
                screenModeDropdown.value = 1;
                break;
            default:
                screenModeDropdown.value = 0;
                break;
        }

        switch (QualitySettings.antiAliasing)
        {
            case 0:
                antiAliasingDropdown.value = 0;
                break;
            case 2:
                antiAliasingDropdown.value = 1;
                break;
            case 4:
                antiAliasingDropdown.value = 2;
                break;
            case 8:
                antiAliasingDropdown.value = 3;
                break;
        }

        qualityDropdown.value = QualitySettings.GetQualityLevel();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    /*public void SetScreenMode(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        
    }*/

    public void SetScreenMode(int index)
    {
        switch(index)
        {
            case 0:
                //Fullscreen
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 1:
                //Windowed
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            default:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
        }

    }


    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetAntiAliasing(int aliasingIndex)
    {
        switch (aliasingIndex)
        {
            case 0:
                QualitySettings.antiAliasing = 0;
                break;
            case 1:
                QualitySettings.antiAliasing = 2;
                break;
            case 2:
                QualitySettings.antiAliasing = 4;
                break;
            case 3:
                QualitySettings.antiAliasing = 8;
                break;
        }
    }
}
