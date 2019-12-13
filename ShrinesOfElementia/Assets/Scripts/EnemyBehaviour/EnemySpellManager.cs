//Author: Sofia Kauko
//Co-authors: Niklas Almqvist & Bilal El Medkouri
using System.Collections.Generic;
using UnityEngine;

public class EnemySpellManager : MonoBehaviour
{

    [SerializeField] private string elementType;
    private float fireballSpeed = 13f;
    private Vector3 fireballSpawnLocation;
    //Wind
    private float windBladeSpeed = 14f;


    private Dictionary<string, System.Action> spells = new Dictionary<string, System.Action>();
    public string ElementType { get => elementType; set => elementType = value; }


    [SerializeField] private GameObject spellPrefab;
    public GameObject SpellPrefab { get => spellPrefab; set => spellPrefab = value; }


    [SerializeField] private float castTime;
    public float CastTime { get => castTime; set => castTime = value; }


    [SerializeField] private float spellDelayModifier;
    public float SpellSpeedModifier { get => spellDelayModifier; set => spellDelayModifier = value; }

    private Vector3 spellPositionPlayer;
    private Vector3 spellPositionElite;
    private Quaternion spellRotation;
    private Vector3 spellAim;

    private Vector3 groundTargetIndicatorPosition;

    [SerializeField] private GameObject groundTargetIndicator = null;

    private void Start()
    {
        spells.Add("Fire", CastFire);
        spells.Add("Water", CastWater);
        spells.Add("Earth", CastEarth);
        spells.Add("Wind", CastWind);
    }

    public void SetAbilityAim()
    {
        spellAim = gameObject.transform.forward;
        spellPositionPlayer = Player.Instance.transform.position;
        spellPositionElite = gameObject.transform.position;
        spellRotation = gameObject.transform.rotation;

        ShowAbilityIndicator();
    }

    public void CastAbility()
    {
        //add indicator
        spells[elementType]();
    }

    private void ShowAbilityIndicator()
    {
        if(elementType == "Earth")
        {
            groundTargetIndicatorPosition = transform.position + transform.forward * 6f + new Vector3(0f, -0.3f);
            Instantiate(groundTargetIndicator, groundTargetIndicatorPosition, spellRotation);
        }
        else if (elementType == "Water")
        {
            Instantiate(groundTargetIndicator, spellPositionPlayer, Quaternion.Euler(-90, transform.rotation.eulerAngles.y, 0));
        }
        
    }
    

    private void CastFire()
    {
        //alter spawn location to avoid colliding with its own collider
        spellPositionElite +=  Vector3.up.normalized * 1.5f + gameObject.transform.forward * 2f;
        Vector3 direction = spellAim * fireballSpeed;

        //cast spell
        Debug.Log("Firespell cast");
        GameObject fireball = Instantiate(spellPrefab, fireballSpawnLocation, this.GetComponent<EnemySM>().Agent.transform.rotation);
        fireball.GetComponent<Fireball>().CasterTag = gameObject.tag;
        fireball.GetComponent<Rigidbody>().AddForce(direction, ForceMode.VelocityChange);
    }


    // could to separeate states for each but there would be a lot of kodupprepning
    private void CastWater()
    {
        Debug.Log("Waterspell cast");
        //spellPositionPlayer +=  Vector3.up * -3f;
        

        //TimerManager.Instance.SetNewTimer(gameObject, spellDelayModifier, CastWaterWithDelay);
        GameObject geyser = Instantiate(spellPrefab, spellPositionPlayer, Quaternion.identity);
        geyser.GetComponent<Geyser>().CasterTag = gameObject.tag;
        EventManager.Instance.FireEvent(new GeyserCastEvent(spellPositionPlayer, 6f)); //6f is range of extinguish. put this in geyser prefab script later.
    }
    

    private void CastEarth()
    {
        /*
        spellRotation = Quaternion.Euler(-90, transform.rotation.eulerAngles.y, 0);
        spellPosition = transform.position + (transform.forward.normalized * 0.5f) + (transform.up * -1f);
        groundTargetIndicatorPosition = transform.position + transform.forward * 6f + new Vector3(0f, -0.3f);
        Instantiate(groundTargetIndicator, groundTargetIndicatorPosition, spellRotation);
        TimerManager.Instance.SetNewTimer(gameObject, spellDelayModifier, CastEarthWithDelay);
        */

        spellPositionElite += transform.forward.normalized * 0.5f + transform.up * -1f;
        GameObject earthSpikes = Instantiate(spellPrefab, spellPositionElite, spellRotation);
        earthSpikes.GetComponent<EarthSpikes>().CasterTag = gameObject.tag;
        earthSpikes.GetComponent<ParticleSystem>().Play(); // shouldnt be needed but is. 

    }
    

    private void CastWind()
    {
        Debug.Log("wind cast");
        spellPositionElite += transform.forward * 1.5f + transform.up * 0.5f;
        GameObject windBlade = Instantiate(spellPrefab, spellPositionElite, spellRotation);
        windBlade.GetComponent<WindBlade>().CasterTag = gameObject.tag;
        windBlade.GetComponent<Rigidbody>().AddForce(spellAim * windBladeSpeed, ForceMode.VelocityChange);
    }


    /*
    private void CastWaterWithDelay()
    {
        GameObject geyser = Instantiate(spellPrefab, spellPosition, Quaternion.identity);
        geyser.GetComponent<Geyser>().CasterTag = gameObject.tag;
        EventManager.Instance.FireEvent(new GeyserCastEvent(spellPosition, 6f)); //6f is range of extinguish. put this in geyser prefab script later.
    }
    */

    /*
    private void CastEarthWithDelay()
    {
        GameObject earthSpikes = Instantiate(spellPrefab, spellPosition, spellRotation);
        earthSpikes.GetComponent<EarthSpikes>().CasterTag = gameObject.tag;
        earthSpikes.GetComponent<ParticleSystem>().Play();
    }
    */

}
