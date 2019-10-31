// Author: Joakim Ljung, Co-author Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [Header("Fireball attributes")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private float fireballSpeed, fireballCooldown;
    private Vector3 fireballSpawnLocation;
    private float fireballTimer;

    [Header("Geyser attributes")]
    [SerializeField] private GameObject geyserPrefab;
    [SerializeField] private float geyserCooldown;
    [SerializeField] private GameObject geyserSpawnLocation; // make this simpler? gsl = player.position + forward * 2f eller ngt baserat på kameran
    [SerializeField]private float moistRange = 6f;
    private float geyserTimer;

    [Header("Elemental Abilities (For Testing)")]
    [SerializeField] private bool hasFire;
    [SerializeField] private bool hasWater;
    [SerializeField] private bool hasEarth;
    [SerializeField] private bool hasWind;


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
            Player.Instance.Animator.SetBool("InCombat", true);
            fireballSpawnLocation = gameObject.transform.position + Vector3.up.normalized * 1.5f + gameObject.transform.forward * 1.8f;
            GameObject fireball = Instantiate(fireballPrefab, fireballSpawnLocation, Quaternion.identity);
            fireball.GetComponent<Rigidbody>().AddForce(CameraReference.Instance.transform.forward * fireballSpeed, ForceMode.VelocityChange);
            fireballTimer = fireballCooldown;
        }
    }

    public void CastGeyser()
    {
        if (hasWater && geyserTimer <= 0f)
        {
            Instantiate(geyserPrefab, geyserSpawnLocation.transform.position, Quaternion.identity);
            geyserTimer = geyserCooldown;
            EventSystem.Current.FireEvent(new GeyserCastEvent(geyserSpawnLocation.transform.position, moistRange)); 
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
