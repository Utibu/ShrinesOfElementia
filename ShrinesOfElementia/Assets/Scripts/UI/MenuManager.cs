//Author: Joakim Ljung
//co-Author: Sofia Kauko

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject extrasMenu;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject continueButton;

    //Achievements panel
    [SerializeField] private GameObject achievementsPanel;
    [SerializeField] private GameObject flightmasterIcon;
    [SerializeField] private GameObject speedrunnerIcon;
    [SerializeField] private GameObject elementalistIcon;
    [SerializeField] private GameObject hundredKillsIcon;
    [SerializeField] private GameObject fireGiantIcon;
    [SerializeField] private GameObject waterGiantIcon;
    [SerializeField] private GameObject windGiantIcon;
    [SerializeField] private GameObject earthGiantIcon;
    [SerializeField] private GameObject giantBaneIcon;

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
        extrasMenu.SetActive(true);
        startMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    public void OnOptions()
    {
        extrasMenu.SetActive(false);
        startMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnNextChapter()
    {
        GameManager.Current.LoadNextLevel();

    }

    public void OnBack()
    {
        extrasMenu.SetActive(false);
        startMenu.SetActive(true);
        optionsMenu.SetActive(false);
        creditsPanel.SetActive(false);
        achievementsPanel.SetActive(false);
    }

    public void OnCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void OnAchievements()
    {
        Debug.Log("ON ACHIEVEMENTS");
        achievementsPanel.SetActive(true);

        GameManager.Current.LoadAchievements();

        //activate icons depending on what is unlocked
        if (AchievementManager.Current.FlightExpert)
        {
            flightmasterIcon.SetActive(true);
            Debug.Log("icon activated");
        }
        if (AchievementManager.Current.Elementalist)
        {
            elementalistIcon.SetActive(true);
            Debug.Log("icon activated");
        }
        if (AchievementManager.Current.SpeedRunner)
        {
            speedrunnerIcon.SetActive(true);
            Debug.Log("icon activated");
        }
        if (AchievementManager.Current.KillcountHundred)
        {
            hundredKillsIcon.SetActive(true);
            Debug.Log("icon activated");
        }
        if (AchievementManager.Current.SlayedGiants["Fire"])
        {
            fireGiantIcon.SetActive(true);
            Debug.Log("icon activated");
        }
        if (AchievementManager.Current.SlayedGiants["Water"])
        {
            waterGiantIcon.SetActive(true);
            Debug.Log("icon activated");
        }
        if (AchievementManager.Current.SlayedGiants["Wind"])
        {
            windGiantIcon.SetActive(true);
            Debug.Log("icon activated");
        }
        if (AchievementManager.Current.SlayedGiants["Earth"])
        {
            earthGiantIcon.SetActive(true);
            Debug.Log("icon activated");
        }
        if(AchievementManager.Current.GiantBane)
        {
            giantBaneIcon.SetActive(true);
        }
    }
 


    //other functions
    public void DisableContinueButton()
    {
        continueButton.SetActive(false);
    }

    
}
