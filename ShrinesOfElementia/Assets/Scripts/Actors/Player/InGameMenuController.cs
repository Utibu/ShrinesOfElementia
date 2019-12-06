using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour
{

    [SerializeField]private GameObject menuPanel;


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
        GameManager.Current.Save();
        SceneManager.LoadScene(0);
    }

    public void OnResumeGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        menuPanel.SetActive(false);
    }


}
