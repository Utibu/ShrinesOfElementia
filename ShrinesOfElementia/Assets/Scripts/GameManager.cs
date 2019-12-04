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


    //things that i put here for now but might be better to move to separate scripts later.
    public GameObject[] bosses;
    public GameObject spawnPointBoss;
    public GameObject PlayerStartingPoint;

    // Start is called before the first frame update
    void Start()
    {
        //secure gamobject
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(spawnPointBoss);
        DontDestroyOnLoad(PlayerStartingPoint);
       
    }
    
    public void LoadNewGame()
    {
        SetDataToDefault();
        SceneManager.LoadScene(1);
        SceneManager.sceneLoaded += SetUpGame;
        
    }

    public void LoadFromSave()
    {
        //Load from save
        LoadVariablesFromSave();

        SceneManager.LoadScene(1);
        SceneManager.sceneLoaded += SetUpGame;
    }

    private void SetUpGame(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Shrines aquired: " + FireUnlocked + " " + EarthUnlocked + " " + WaterUnlocked + " " + WindUnlocked);

        //register listeners        
        EventManager.Current.RegisterListener<ShrineEvent>(RegisterShrine);
        EventManager.Current.RegisterListener<PlayerDeathEvent>(RespawnPlayer);
        EventManager.Current.RegisterListener<ExperienceEvent>(OnPlayerXPEvent);


        //load boss and abilities
        LoadBoss();
        //move player to last saved checkpoint
        Player.Instance.transform.position = NearestCheckpoint;
        Player.Instance.Health.CurrentHealth = PlayerHP;
        TimerManager.Current.SetNewTimer(gameObject, 1f, LoadAbilities); // Event seems to not be heard if sent too early...
        
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
    public void LoadBoss()
    {
        //load boss and spawn enemies according to nr of srines unlocked and what level is laoding.
        Instantiate(bosses[Level], spawnPointBoss.transform);
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

    }

    private void RespawnPlayer(PlayerDeathEvent ev)
    {
        Debug.Log(ev.Player.name + " Is respawning from GameManager");
        Vector3 spawnpoint = CheckpointManager.Current.FindNearestSpawnPoint();
        Player.Instance.transform.position = spawnpoint;
    }

    public void OnPlayerDeath()
    {
        PlayerDeaths += 1;
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
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            //get Variables from save:
            Level = Level;
            PlayerLevel = data.PlayerLevel;
            PlayerXP = data.PlayerXP;
            PlayerHP = data.PlayerHP;
            PlayerDeaths = data.PlayerDeaths;
            FireUnlocked = data.FireUnlocked;
            WaterUnlocked = data.WaterUnlocked;
            WindUnlocked = data.WindUnlocked;
            EarthUnlocked = data.EarthUnlocked;
            NearestCheckpoint = new Vector3(data.SpawnX, data.SpawnY, data.SpawnZ);
            Debug.Log(data.SpawnX + " " +  data.SpawnY +" "+  data.SpawnZ);
        }
        else
        {
            Debug.Log("SAVEFILE DOES NOT EXIST");
        }
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
