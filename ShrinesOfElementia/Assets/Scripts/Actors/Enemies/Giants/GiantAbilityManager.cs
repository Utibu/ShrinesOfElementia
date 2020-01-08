// Author: Bilal El Medkouri
// Co-Author: Sofia Chyle Kauko

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GiantAbilityManager : MonoBehaviour
{


    //new stuff
    public bool Ready { get; private set; }
    private Dictionary<string, System.Action> abilities;
    private Quaternion abilityRotation;
    private Vector3 abilityDirection;
    private Vector3 abilityPositionPlayer;
    private Vector3 abilityPositionGiant;
    private string ElementalType;

    //old stuff
    public static GiantAbilityManager Instance { get; private set; }
    

    
    [SerializeField] private GameObject giantAbility;
    [Range(0f, 100f)] public float AbilityRange = 0f;
    [SerializeField] private float abilityMinRange;
    public float AbilityMinRange { get => abilityMinRange; set => abilityMinRange = value; }
    [Range(0f, 60f)] [SerializeField] protected float abilityCooldown = 0f;

    
    protected void Awake() // was virtual.
    {
        // Prevents multiple instances
        if (Instance == null) { Instance = this; }
        else { Debug.Log("Warning: multiple " + this + " in scene!"); }

        //set up dictionary
        abilities = new Dictionary<string, System.Action>();
        abilities.Add("Fire", CastFire);
        abilities.Add("Water", CastWater);
        abilities.Add("Wind", CastWind);
        abilities.Add("Earth", CastEarth);

        Ready = true;
        ElementalType = GetComponent<Giant>().ElementalType;
    }

    public void SetAim()
    {
        abilityPositionGiant = gameObject.transform.position;
        abilityPositionPlayer = Player.Instance.transform.position;
        abilityDirection = gameObject.transform.forward;
        abilityRotation = gameObject.transform.rotation;
    }

    public void CastAbility()
    {
        Ready = false;
        TimerManager.Instance.SetNewTimer(gameObject, abilityCooldown, ResetReady);
        abilities[ElementalType]();
    }

    private void ResetReady()
    {
        Ready = true;
    }


    //break this out if time available
    private void CastFire()
    {
        abilityPositionGiant += gameObject.transform.forward * 5f + Vector3.up * 2f;
        GameObject fireball = GameObject.Instantiate(giantAbility, abilityPositionGiant, abilityRotation);
        fireball.GetComponent<Fireball>().CasterTag = gameObject.tag;
        fireball.GetComponent<Rigidbody>().AddForce(abilityDirection * 14f, ForceMode.VelocityChange);
    }

    private void CastWater()
    {
        GameObject water = GameObject.Instantiate(giantAbility, abilityPositionGiant, abilityRotation);
    }

    private void CastWind()
    {
        abilityPositionGiant += (Vector3.up * 2f + gameObject.transform.forward * 3f);
        GameObject windAbility = GameObject.Instantiate(giantAbility, abilityPositionGiant, abilityRotation);
        windAbility.GetComponent<GiantWindBlade>().CasterTag = gameObject.tag;
        windAbility.GetComponent<Rigidbody>().AddForce(abilityDirection * 14f, ForceMode.VelocityChange);
    }

    private void CastEarth()
    {
        GameObject earthAbility = GameObject.Instantiate(giantAbility, abilityPositionGiant, abilityRotation);
        earthAbility.GetComponent<EarthSpikes>().CasterTag = gameObject.tag;
    }

    //slow down and stop when hitting ground.
    private void StopLeapSpeed()
    {
        Giant giant = gameObject.GetComponent<Giant>();
        giant.Agent.speed = 0;
        giant.Agent.SetDestination(gameObject.transform.position + gameObject.transform.forward * 2f);
    }

    



}
