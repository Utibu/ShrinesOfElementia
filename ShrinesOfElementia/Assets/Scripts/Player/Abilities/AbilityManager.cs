using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [Header("Temporary Fireball attributes")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private float fireballSpeed, fireballCooldown;
    private Vector3 fireballSpawnLocation;
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
        fireballTimer -= Time.deltaTime;
        geyserTimer -= Time.deltaTime;
    }

    public void CastFireBall()
    {
        if (hasFire && fireballTimer <= 0f)
        {
            fireballSpawnLocation = gameObject.transform.position + Vector3.up.normalized * 1.5f + gameObject.transform.forward * 2f; ;
            GameObject fireball = Instantiate(fireballPrefab, fireballSpawnLocation, Quaternion.identity);
            fireball.GetComponent<Rigidbody>().AddForce(transform.forward * fireballSpeed, ForceMode.VelocityChange);
            fireballTimer = fireballCooldown;
        }
    }

    public void CastGeyser()
    {
        if (hasWater && geyserTimer <= 0f)
        {
            Instantiate(geyserPrefab, geyserSpawnLocation.transform.position, Quaternion.identity);

            geyserTimer = geyserCooldown;
        }
    }
    private void EnableFireAbilities()
    {
        hasFire = true;
    }

    private void EnableWaterAbilities()
    {
        hasWater = true;
        Physics.IgnoreLayerCollision(9, 4, false);
        hasFire = true; // just for testing!
    }

    private void EnableEarthAbilities()
    {
        hasEarth = true;
    }

    private void EnableWindAbilities()
    {
        hasWind = true;
    }


    private void unlockElement(ShrineEvent eve)
    {
        switch (eve.Element)
        {
            case "Fire":
                EnableFireAbilities();
                break;
            case "Water":
                EnableWaterAbilities();
                break;
            case "Earth":
                EnableEarthAbilities();
                break;
            case "Wind":
                EnableWindAbilities();
                break;
        }
    }
}
