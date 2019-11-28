//Author: Sofia Kauko
//Co-author: Niklas Almqvist
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpellManager : MonoBehaviour
{
    private Vector3 aim;

    [SerializeField] private string elementType;
    private float fireballSpeed = 13f;
    private Vector3 fireballSpawnLocation;
    //Water

    //Earth
    //Wind
    private float windBladeSpeed = 10f;


    private Dictionary<string, System.Action> spells = new Dictionary<string, System.Action>();
    public string ElementType { get => elementType; set => elementType = value; }
    

    [SerializeField] private GameObject spellPrefab;
    public GameObject SpellPrefab { get => spellPrefab; set => spellPrefab = value; }
    

    [SerializeField] private float castTime;
    public float CastTime { get => castTime; set => castTime = value; }
    

    [SerializeField] private float spellSpeedModifier;
    public float SpellSpeedModifier { get => spellSpeedModifier; set => spellSpeedModifier = value; }


    // Start is called before the first frame update
    void Start()
    {
        spells.Add("Fire", CastFire);
        spells.Add("Water", CastWater);
        spells.Add("Earth", CastEarth);
        spells.Add("Wind", CastWind);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CastAbility()
    {
        spells[elementType]();
    }

    public void SetAim(Vector3 aim)
    {
        this.aim = aim.normalized;
    }

    private void CastFire()
    {
        //alter spawn location to avoid colliding with its own collider
        fireballSpawnLocation = transform.position + Vector3.up.normalized * 1.5f + gameObject.transform.forward * 2f;
        Vector3 direction = aim * fireballSpeed;
        //cast spell
        Debug.Log("Firespell cast");
        GameObject fireball = Instantiate(spellPrefab, fireballSpawnLocation, this.GetComponent<EnemySM>().Agent.transform.rotation);
        fireball.GetComponent<Fireball>().Caster = gameObject;
        fireball.GetComponent<Rigidbody>().AddForce(direction, ForceMode.VelocityChange);
    }


    // could to separeate states for each but there would be a lot of kodupprepning
    private void CastWater()
    {
        Debug.Log("Waterspell cast");
        Vector3 eruptionSpot = Player.Instance.transform.position + Vector3.up * -3f;
        GameObject geyser = Instantiate(spellPrefab, eruptionSpot, Quaternion.identity);
        geyser.GetComponent<Geyser>().Caster = gameObject;
        EventManager.Current.FireEvent(new GeyserCastEvent(eruptionSpot, 6f)); //6f is range of extinguish. put this in geysher prefab script later.
    }

    private void CastEarth()
    {
        Debug.Log("earth cast");
        Quaternion spikesRotation = Quaternion.Euler(-90, transform.rotation.eulerAngles.y, 0);
        Vector3 originPosition = transform.position + (transform.forward.normalized * 0.5f) + (transform.up * -1f);
        GameObject earthSpikes = Instantiate(spellPrefab, originPosition, spikesRotation);
        earthSpikes.GetComponent<EarthSpikes>().Caster = gameObject;
        earthSpikes.GetComponent<ParticleSystem>().Play();
    }

    private void CastWind()
    {
        Debug.Log("wind cast");
        GameObject windBlade = Instantiate(spellPrefab, transform.position + Vector3.up.normalized + transform.forward * 1f + transform.up * -0.3f, this.GetComponent<EnemySM>().Agent.transform.rotation);
        windBlade.GetComponent<WindBlade>().Caster = gameObject;
        windBlade.GetComponent<Rigidbody>().AddForce(aim * windBladeSpeed, ForceMode.VelocityChange);
    }

}
