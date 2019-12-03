//Author: Joakim Ljung
//co-Author: Sofia Kauko

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnStart()
    {
        GameManager.Current.LoadNewGame();
        //SceneManager.LoadScene(1);
    }

    public void OnContinue()
    {
        GameManager.Current.LoadFromSave();
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
