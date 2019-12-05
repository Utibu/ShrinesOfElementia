//Author: Joakim Ljung
//co-Author: Sofia Kauko

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject OptionsMenu;
    [SerializeField] private GameObject ExtrasMenu;
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

    //On Click functions
    public void OnContinue()
    {
        GameManager.Current.LoadFromSave();
    }

    public void OnStart()
    {
        GameManager.Current.LoadNewGame();
    }

    public void OnExtras()
    {
        ExtrasMenu.SetActive(true);
        startMenu.SetActive(false);
        OptionsMenu.SetActive(false);
    }

    public void OnOptions()
    {
        ExtrasMenu.SetActive(false);
        startMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnBack()
    {
        ExtrasMenu.SetActive(false);
        startMenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }


    //other functions
    public void DisableContinueButton()
    {
        continueButton.SetActive(false);
    }
}
