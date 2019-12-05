//Author: Sofia Kauko
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SaveData
{
    public int Level;
    public int PlayerLevel;
    public int PlayerXP;
    public int PlayerHP;
    public int PlayerDeaths;
    public bool FireUnlocked;
    public bool WaterUnlocked;
    public bool WindUnlocked;
    public bool EarthUnlocked;
    public float SpawnX;
    public float SpawnY;
    public float SpawnZ;
    //achievement data
    public bool SpeedRunner;
    //Elementalist
    public bool Elementalist;
    //Flight expert
    public bool FlightExpert;
    //Elementals hate her!
    public bool KillcountHundred;
    public int currentKills;
    //Giantslayer
    public bool SlayedFireGiant;
    public bool SlayedWaterGiant;
    public bool SlayedWindGiant;
    public bool SlayedEarthGiant;




}
