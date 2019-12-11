//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class GameManager : MonoBehaviour
{

    private static GameManager current;
    public static GameManager Current
    {
        get
        {
            if (current == null)
            {
                current = GameObject.FindObjectOfType<GameManager>();
            }
            return current;
        }
    }

    private SaveData saveData;

    //References to relevant stuff
    public AchievementManager Achievements;
    [SerializeField] private int MaxAllowedDeaths;
    private bool SaveDataExists = false;
    public GameObject PlayerStartingPoint;

    //variables
    public int Level { get; set; }
    public int PlayerLevel { get; set; }
    public int PlayerXP { get; set; }
    public int PlayerHP { get; set; }
    public int PlayerDeaths { get; set; }
    public bool FireUnlocked { get; set; }
    public bool WaterUnlocked { get; set; }
    public bool WindUnlocked { get; set; }
    public bool EarthUnlocked { get; set; }
    public Vector3 NearestCheckpoint { get; set; }


    

    // Start is called before the first frame update
    void Start()
    {
        //secure gamobject
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(PlayerStartingPoint);

        //gameObject.AddComponent<AchievementManager>();
        //Achievements = GetComponent<AchievementManager>();

        //Achievements = AchievementManager.Current;


        Debug.Log("start function -  deaths: " + PlayerDeaths);

        //check if game has a save
        LoadVariablesFromSave();

        if (SaveDataExists)
        {
            AchievementManager.Current.InitializeFromSave(saveData);
        }
        else
        {
            MenuManager.Current.DisableContinueButton();
        }

    }
    
    public void LoadNewGame()
    {
        //init  acievement manager default
        Achievements.InitializeToDefault();
        //set data to default
        SetDataToDefault();

        //load scene
        SceneManager.LoadScene(1);
        SceneManager.sceneLoaded += SetUpGame;

        
    }

    public void LoadFromSave()
    {
        //Loaded already in start, just init the rest.
        //LoadVariablesFromSave();
        SceneManager.LoadScene(1);
        SceneManager.sceneLoaded += SetUpGame;

        
    }

    public void LoadNextLevel()
    {
        NearestCheckpoint = PlayerStartingPoint.transform.position; // set up player spawn at basecamp
        PlayerHP = 150;
        Save();  
        LoadFromSave();
    }

    public void LoadAchievements()
    {
        LoadVariablesFromSave();
        AchievementManager.Current.InitializeFromSave(saveData);
    }

    //Called from both new game and continue with save. Prepares gameworld according to previosly loaded data.(if no save, default data.)
    private void SetUpGame(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 1) // set up game world if game world was loaded.
        {
            Debug.Log("Shrines aquired: " + FireUnlocked + " " + EarthUnlocked + " " + WaterUnlocked + " " + WindUnlocked);

            //register listeners        
            EventManager.Current.RegisterListener<ShrineEvent>(RegisterShrine);
            EventManager.Current.RegisterListener<PlayerDeathEvent>(OnPlayerDeath);
            EventManager.Current.RegisterListener<ExperienceEvent>(OnPlayerXPEvent);

            //prepare player position and HP
            Player.Instance.transform.position = NearestCheckpoint;
            Player.Instance.Health.CurrentHealth = PlayerHP;

            //load boss and player abilities
            //LoadBoss();
            TimerManager.Current.SetNewTimer(gameObject, 1f, LoadAbilities); // Event seems to not be heard if sent too early...
            Debug.Log("set up game finished -  deaths: " + PlayerDeaths);
        }
        
        
    }

    private void LoadAbilities()
    {
        if (FireUnlocked)
        {
            EventManager.Current.FireEvent(new ShrineEvent("Fire enabled from gameManager", "Fire"));
        }
        if (WaterUnlocked)
        {
            EventManager.Current.FireEvent(new ShrineEvent("Water enabled from gameManager", "Water"));
        }
        if (WindUnlocked)
        {
            EventManager.Current.FireEvent(new ShrineEvent("Wind enabled from gameManager", "Wind"));
        }
        if (EarthUnlocked)
        {
            EventManager.Current.FireEvent(new ShrineEvent("Earth enabled from gameManager", "Earth"));
        }

        

    }

    private void RegisterShrine(ShrineEvent ev)
    {
        switch (ev.Element){
            case "Fire":
                FireUnlocked = true;
                break;
            case "Earth":
                EarthUnlocked = true;
                break;
            case "Wind":
                WindUnlocked = true;
                break;
            case "Water":
                WaterUnlocked = true;
                break;
            default:
                Debug.Log("Element not valid");
                break;
        }


        Save();

    }


    //Should be called from checkpoint manager each time a new checkpoint is reached. Update nearest and save game.
    public void SaveLatestCheckpoint(Vector3 checkpoint)
    {
        NearestCheckpoint = checkpoint;
        Save();
    }


    //Called from healtrh component each time a boss dies. Level increases and game saves. Add UI to continue or small cutscene or similar.
    public void OnBossDeath() 
    {
        Level += 1;
        Debug.Log("Boss died, level int has been increased");
        //go to menu? play cutscene? show UI with "next Chapter"? 
        Save();
        SceneManager.LoadScene(3);

    }

    private void RespawnPlayer()
    {
        Vector3 spawnpoint = CheckpointManager.Current.FindNearestSpawnPoint();
        Player.Instance.Health.CurrentHealth = Player.Instance.Health.MaxHealth;
        Player.Instance.transform.position = spawnpoint;
    }

    public void OnPlayerDeath(PlayerDeathEvent ev)
    {
        PlayerDeaths += 1;
        Debug.Log("OnPlayer death called -  deaths: " + PlayerDeaths);
        if (PlayerDeaths > MaxAllowedDeaths)
        {
            DeleteSave();
            SceneManager.LoadScene(2);
            return;
        }
        RespawnPlayer();
    }

    public void OnPlayerLevelUp() 
    {
        PlayerLevel += 1;
        PlayerXP = 0;

    }
    public void OnPlayerXPEvent(ExperienceEvent ev)
    {
        PlayerXP += (int)ev.Experience;
    }
    

    
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        SaveData data = new SaveData();

        //get HP from Player
        PlayerHP = Player.Instance.Health.CurrentHealth;

        //set save variables
        data.ContainsSaveData = true;
        data.Level = Level;
        data.PlayerLevel = PlayerLevel;
        data.PlayerXP = PlayerXP;
        data.PlayerHP = PlayerHP;
        data.PlayerDeaths = PlayerDeaths;
        data.FireUnlocked = FireUnlocked;
        data.WaterUnlocked = WaterUnlocked;
        data.WindUnlocked = WindUnlocked;
        data.EarthUnlocked = EarthUnlocked;
        //helvete. (vector3 cant serialize, saving float values instead)
        data.SpawnX = NearestCheckpoint.x;
        data.SpawnY = NearestCheckpoint.y;
        data.SpawnZ = NearestCheckpoint.z;

        //save achievement data
        data.SpeedRunner = Achievements.SpeedRunner;
        data.Elementalist = Achievements.Elementalist;
        data.FlightExpert = Achievements.FlightExpert;
        data.KillcountHundred = Achievements.KillcountHundred;
        data.currentKills = Achievements.currentKills;
        bool fireKilled;
        Achievements.SlayedGiants.TryGetValue("Fire", out fireKilled); 
        data.SlayedFireGiant = fireKilled;
        bool waterKilled;
        Achievements.SlayedGiants.TryGetValue("Fire", out waterKilled);
        data.SlayedWaterGiant = waterKilled;
        bool windKilled;
        Achievements.SlayedGiants.TryGetValue("Fire", out windKilled);
        data.SlayedWindGiant = windKilled;
        bool earthKilled;
        Achievements.SlayedGiants.TryGetValue("Fire", out earthKilled);
        data.SlayedEarthGiant = earthKilled;
        data.GiantBane = Achievements.GiantBane;


        Debug.Log("SAVING: shrine:" + data.FireUnlocked + " " + data.EarthUnlocked + " " + data.WaterUnlocked + " " + data.WindUnlocked);
        //stream to file
        bf.Serialize(file, data);
        file.Close();
    }
    private void LoadVariablesFromSave()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            Debug.Log("SAVEFILE EXISTS");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            saveData = (SaveData)bf.Deserialize(file);
            file.Close();

            //makes sure gameManager knows if data is a save or just default null + 0
            SaveDataExists = saveData.ContainsSaveData;

            //get Variables from save:
            Level = Level;
            PlayerLevel = saveData.PlayerLevel;
            PlayerXP = saveData.PlayerXP;
            PlayerHP = saveData.PlayerHP;
            PlayerDeaths = saveData.PlayerDeaths;
            FireUnlocked = saveData.FireUnlocked;
            WaterUnlocked = saveData.WaterUnlocked;
            WindUnlocked = saveData.WindUnlocked;
            EarthUnlocked = saveData.EarthUnlocked;
            NearestCheckpoint = new Vector3(saveData.SpawnX, saveData.SpawnY, saveData.SpawnZ);

            //init achievement data
            AchievementManager.Current.InitializeFromSave(saveData);
        }
        else
        {
            SaveDataExists = false;
            Debug.Log("SAVEFILE DOES NOT EXIST");
        }
    }


    private void DeleteSave()
    {
        Debug.Log("overriding (deleting) savedata");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        //create empty savedata to override with. Ik, dumb solution but its safe.
        SaveData data = new SaveData();
        data.PlayerDeaths = 0;
        //stream to file
        bf.Serialize(file, data);
        file.Close();
    }

    private void SetDataToDefault()
    {
        Level = 0;
        PlayerLevel = 1;
        PlayerXP = 0;
        PlayerHP = 150; // generalize
        PlayerDeaths = 0;
        FireUnlocked = false;
        WaterUnlocked = false;
        WindUnlocked = false;
        EarthUnlocked = false;
        NearestCheckpoint = PlayerStartingPoint.transform.position;
    }

    

}
