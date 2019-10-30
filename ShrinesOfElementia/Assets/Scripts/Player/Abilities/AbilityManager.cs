using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [Header("Temporary Fireball attributes")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private float fireballSpeed, fireballCooldown;
    [SerializeField] private GameObject fireballSpawnLocation;
    private float fireballTimer;

    [Header("Temporary Geyser attributes")]
    [SerializeField] private GameObject geyserPrefab;
    [SerializeField] private float geyserCooldown;
    [SerializeField] private GameObject geyserSpawnLocation;
    private float geyserTimer;

    private bool hasFire;
    private bool hasWater;
    private bool hasEarth;
    private bool hasWind;


    private void Start()
    {
        fireballTimer = 0.0f;
        geyserTimer = 0.0f;
        EventSystem.Current.RegisterListener<ShrineEvent>(unlockElement);
    }

    private void Update()
    {
        
    }

    public void CastFireBall()
    {

    }

    public void CastGeyser()
    {

    }

    private void unlockElement(ShrineEvent eve)
    {
        switch (eve.Element)
        {
            case "Fire":
                hasFire = true;
                break;
            case "Water":
                hasWater = true;
                break;
            case "Earth":
                hasEarth = true;
                break;
            case "Wind":
                hasWind = true;
                break;
        }
    }
}
