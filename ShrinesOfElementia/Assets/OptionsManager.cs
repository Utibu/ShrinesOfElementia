﻿//Author: Joakim Ljung

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsManager : MonoBehaviour
{

    Resolution[] resolutions;
    //[SerializeField] private Dropdown resolutionDropdown;
    [SerializeField]private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown antiAliasingDropdown;
    [SerializeField] private TMP_Dropdown screenModeDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;



    private void Start()
    {
        LoadSettings();
    }

    public void LoadSettings()
    {
        resolutions = Screen.resolutions;
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();


        switch (Screen.fullScreenMode)
        {
            case FullScreenMode.FullScreenWindow:
                screenModeDropdown.value = 2;
                break;
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
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                //Windowed
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            case 2:
                //Borderless
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            default:
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
