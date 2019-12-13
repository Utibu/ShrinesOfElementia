// Main Author: Joakim Ljung
// Co-Author: Sofia Kauko

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

    public static MenuManager Instance { get; private set; }

    private void Awake()
    {
        // Prevents multiple instances
        if (Instance == null) { Instance = this; }
        else { Debug.Log("Warning: multiple " + this + " in scene!"); }



        //make sure continue button only shows if in menuscene and save exists
        if (continueButton != null)
        {
            continueButton.SetActive(false); // should be false by default.
            if (GameManager.Instance.SaveDataExists)
            {
                continueButton.SetActive(true);
            }
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
        GameManager.Instance.LoadFromSave();
    }

    public void OnStart()
    {
        GameManager.Instance.LoadNewGame();
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
        GameManager.Instance.LoadNextLevel();

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

        GameManager.Instance.LoadAchievements();

        flightmasterIcon.SetActive(false);
        speedrunnerIcon.SetActive(false);
        elementalistIcon.SetActive(false);
        hundredKillsIcon.SetActive(false);
        fireGiantIcon.SetActive(false);
        waterGiantIcon.SetActive(false);
        earthGiantIcon.SetActive(false);
        giantBaneIcon.SetActive(false);

        //activate icons depending on what is unlocked
        if (AchievementManager.Instance.FlightExpert)
        {
            flightmasterIcon.SetActive(true);
            Debug.Log("icon activated");
        }
        if (AchievementManager.Instance.Elementalist)
        {
            elementalistIcon.SetActive(true);
            Debug.Log("icon activated");
        }
        if (AchievementManager.Instance.SpeedRunner)
        {
            speedrunnerIcon.SetActive(true);
            Debug.Log("icon activated");
        }
        if (AchievementManager.Instance.KillcountHundred)
        {
            hundredKillsIcon.SetActive(true);
            Debug.Log("icon activated");
        }
        if (AchievementManager.Instance.SlayedGiants["Fire"])
        {
            fireGiantIcon.SetActive(true);
            Debug.Log("icon activated");
        }
        if (AchievementManager.Instance.SlayedGiants["Water"])
        {
            waterGiantIcon.SetActive(true);
            Debug.Log("icon activated");
        }
        if (AchievementManager.Instance.SlayedGiants["Wind"])
        {
            windGiantIcon.SetActive(true);
            Debug.Log("icon activated");
        }
        if (AchievementManager.Instance.SlayedGiants["Earth"])
        {
            earthGiantIcon.SetActive(true);
            Debug.Log("icon activated");
        }
        if (AchievementManager.Instance.GiantBane)
        {
            giantBaneIcon.SetActive(true);
        }
    }



    //other functions
    public void ActivateContinueButton()
    {
        continueButton.SetActive(true);
    }


}
