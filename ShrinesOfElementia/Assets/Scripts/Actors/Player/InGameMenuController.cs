﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class InGameMenuController : MonoBehaviour
{

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject soundPanel;


    private void Update()
    {
        if (Input.GetKeyDown(InputManager.Instance.keyCode["Pause"].keyCode))
        {
            if (menuPanel.activeSelf == false && GameManager.Instance.CanOpenInterface)
            {
                ShowInGameMenu();
                print("opening");
            }
            else if(menuPanel.activeSelf == true)
                HideInGameMenu();
        }
    }


    public void ShowInGameMenu()
    {
        CameraReference.Instance.gameObject.GetComponent<CameraManager>().enabled = false;
        CameraReference.Instance.gameObject.GetComponent<FreeLookAxisDriver>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        menuPanel.SetActive(true);
       
    }
    private void HideInGameMenu()
    {
        CameraReference.Instance.gameObject.GetComponent<CameraManager>().enabled = true;
        CameraReference.Instance.gameObject.GetComponent<FreeLookAxisDriver>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        menuPanel.SetActive(false);
        
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
        HideInGameMenu();
    }

}
