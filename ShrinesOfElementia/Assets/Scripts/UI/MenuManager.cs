//Author: Joakim Ljung
//co-Author: Sofia Kauko

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject continueButton;

    private static MenuManager current;
    public static MenuManager Current
    {
        get
        {
            if (current == null)
            {
                current = GameObject.FindObjectOfType<MenuManager>();
            }
            return current;
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //when new game is klicked
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

    public void DisableContinueButton()
    {
        continueButton.SetActive(false);
    }
}
