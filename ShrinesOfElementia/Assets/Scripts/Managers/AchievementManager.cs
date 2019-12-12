//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{

    //Speedrunner: finish gamerun within 5 minutes
    public bool SpeedRunner { get; set; }
    private bool SpeedRunnerTimerActive;
    [SerializeField] private float maxRunTime;
    private GameObject speedrunnerTimer;

    //Elementalist: get all shrines in 1 run
    public bool Elementalist { get; set; }
    public ArrayList collectedElements { get; set; }

    //Flight expert (just set this true from movement script that handles glide)
    public bool FlightExpert { get; set; }

    //Elementals hate her!: kill 100 elementals
    public bool KillcountHundred { get; set; }
    [SerializeField] private int requiredKills;
    public int currentKills { get; set; }

    //Giantslayer: one achievement per giant killed 
    public Dictionary<string, bool> SlayedGiants { get; set; }
    public bool GiantBane { get; set; }


    public static AchievementManager Instance { get; private set; }

    private void Awake()
    {
        // Prevents multiple instances
        if (Instance == null) { Instance = this; }
        else { Debug.Log("Warning: multiple " + this + " in scene!"); }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        InitializeToDefault();
    }

    public void InitializeToDefault()
    {
        //set up speedrunner achievement
        SpeedRunnerTimerActive = true; //is level finishes before this bool changes to false, player met the speedrunenr achievement goal

        // set up Elementalist
        collectedElements = new ArrayList();

        //set up Elementals hater her! : KillcountHundred
        currentKills = 0;

        //set up Giantslayer. 
        SlayedGiants = new Dictionary<string, bool>();
        SlayedGiants.Add("Fire", false);
        SlayedGiants.Add("Water", false);
        SlayedGiants.Add("Wind", false);
        SlayedGiants.Add("Earth", false);
        GiantBane = false;

    }

    //if Gamemanager loads game from save:
    public void InitializeFromSave(SaveData data)
    {
        //set up speedrunner achievement
        SpeedRunner = data.SpeedRunner;

        //set up Elementals hater her! : KillcountHundred
        currentKills = data.currentKills;

        //set up elementalist
        Elementalist = data.Elementalist;

        //set up Giantslayer
        SlayedGiants = new Dictionary<string, bool>();
        SlayedGiants.Add("Fire", data.SlayedFireGiant);
        SlayedGiants.Add("Water", data.SlayedWaterGiant);
        SlayedGiants.Add("Wind", data.SlayedWindGiant);
        SlayedGiants.Add("Earth", data.SlayedEarthGiant);
        GiantBane = data.GiantBane;
    }


    //when gamelevel loads, start speedrunner-timer. REMEBER: change int if buildscene order changes. 
    private void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            speedrunnerTimer = TimerManager.Instance.SetNewTimer(gameObject, maxRunTime, SpeedRunnerOutOfTime);

            EventManager.Instance.RegisterListener<ShrineEvent>(AddCollectedShrine);
            EventManager.Instance.RegisterListener<EnemyDeathEvent>(IncreaseKillcount);
            EventManager.Instance.RegisterListener<BossDeathEvent>(UnlockGiantslayer);
        }
    }


    private void AddCollectedShrine(ShrineEvent ev)
    {
        //add element if its not already in list. (some shrine events fire several times :/ )
        if (!collectedElements.Contains(ev.Element))
        {
            collectedElements.Add(ev.Element);

            //you have all 4 elements, gain Elementalist
            if (collectedElements.Count >= 4)
            {
                Elementalist = true;
            }
        }
    }

    private void SpeedRunnerOutOfTime()
    {
        SpeedRunnerTimerActive = false;
    }

    public void FlightExpertTrue()
    {
        FlightExpert = true;
    }


    private void IncreaseKillcount(EnemyDeathEvent ev)
    {
        currentKills += 1;
        if (currentKills >= requiredKills)
        {
            KillcountHundred = true;
        }
    }

    private void UnlockGiantslayer(BossDeathEvent ev)
    {
        SlayedGiants[ev.ElementType] = true;
        if (!SlayedGiants.ContainsValue(false))
        {
            GiantBane = true;
        }

        //if boss killed and speedrunnertimer = active, congratulations
        SpeedRunner = true;
    }
}
