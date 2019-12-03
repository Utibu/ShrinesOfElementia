//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
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
    public int PlayerDeaths { get; set; }
    public bool FireUnlocked { get; set; }
    public bool WaterUnlocked { get; set; }
    public bool WindUnlocked { get; set; }
    public bool EarthUnlocked { get; set; }


    //things that i put here for now but might be better to move to separate scripts later.
    public GameObject[] bosses;
    public GameObject BossSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        //load alll variables from save, register listeners
        Load();
        EventManager.Current.RegisterListener<ShrineEvent>(RegisterShrine);
        EventManager.Current.RegisterListener<PlayerDeathEvent>(OnBossDeath);


        //prepare game world (spawn the right boss for level, unlock previosly unlocked shrines from last level...)
        LoadBoss();
        TimerManager.Current.SetNewTimer(gameObject, 0.2f, LoadAbilities);


        

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
        Instantiate(bosses[Level], BossSpawnPoint.transform);
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
    }

    public void OnBossDeath(PlayerDeathEvent ev) // creating boss death event later
    {
        Level += 1;
    }

    public void OnPlayerDeath()
    {
        PlayerDeaths += 1;
    }

    public void OnPlayerLevelUp() 
    {
        PlayerLevel += 1;
    }

    

    
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        SaveData data = new SaveData();

        //set save variables
        data.Level = Level;
        data.PlayerLevel = PlayerLevel;
        data.PlayerDeaths = PlayerDeaths;
        data.FireUnlocked = FireUnlocked;
        data.WaterUnlocked = WaterUnlocked;
        data.WindUnlocked = WindUnlocked;
        data.EarthUnlocked = EarthUnlocked;


        Debug.Log("SAVING: shrine:" + data.FireUnlocked + " " + data.EarthUnlocked + " " + data.WaterUnlocked + " " + data.WindUnlocked);
        //stream to file
        bf.Serialize(file, data);
        file.Close();
    }
    private void Load()
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
            PlayerDeaths = data.PlayerDeaths;
            FireUnlocked = data.FireUnlocked;
            WaterUnlocked = data.WaterUnlocked;
            WindUnlocked = data.WindUnlocked;
            EarthUnlocked = data.EarthUnlocked;
        }
        else
        {
            Debug.Log("SAVEFILE DOES NOT EXIST");
        }
    }



}
