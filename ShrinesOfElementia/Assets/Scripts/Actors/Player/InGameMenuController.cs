﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour
{

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject soundPanel;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            ShowInGameMenu();
        }
    }


    public void ShowInGameMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        menuPanel.SetActive(true);
    }

    public void OnSaveAndExit()
    {
        GameManager.Instance.Save();
        SceneManager.LoadScene(0);
    }

    public void OnSoundOptionsClick()
    {
        menuPanel.SetActive(false);
        soundPanel.SetActive(true);
    }

    public void OnBackClick()
    {
        menuPanel.SetActive(true);
        soundPanel.SetActive(false);
    }

    public void OnResumeGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        menuPanel.SetActive(false);
    }


}
