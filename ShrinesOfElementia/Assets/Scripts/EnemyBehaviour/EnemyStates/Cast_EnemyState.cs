//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "InstantiateEnemyStates/Cast_EnemyState")]
public class Cast_EnemyState : BasicEnemyBaseState
{

    private GameObject spellPrefab;
    private float castTime;
    private float countdown;
    private Dictionary<string, System.Action> spells = new Dictionary<string, System.Action>();
    private string elementType;

    //spellvariables. consider including these in the actual spell prefab/ spellprefab script later.(unless palyer and elites have different versions of same spells)///
    //Fire
    private float fireballSpeed = 13f;
    private Vector3 fireballSpawnLocation;
    //Water

    //Earth
    //Wind
    private float windBladeSpeed = 10f;
    

    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);

        //Prepare functions to cast the right spell
        spells.Add("Fire", CastFire);
        spells.Add("Water", CastWater);
        spells.Add("Earth", CastEarth);
        spells.Add("Wind", CastWind);

        //get nessecary variables
        EnemySpellManager spelldata = owner.GetComponent<EnemySpellManager>();
        elementType = spelldata.ElementType;
        spellPrefab = spelldata.SpellPrefab;
        castTime = spelldata.CastTime;
    }

    public override void Enter()
    {
        base.Enter();
        //stop and rotate / aim to player
        owner.Agent.destination = owner.Player.transform.position;
        owner.Agent.isStopped = true; // stop while casting

        //set timer
        countdown = 0.5f;

        
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();
        //rotate / aim at player
        owner.transform.LookAt(owner.Player.transform.position);
        
        //tick down spell channel
        countdown -= Time.deltaTime;

        
        
        //spellcast:
        if(countdown <= 0)
        {
            Debug.Log("Element type: " + elementType);
            spells[elementType]();
            countdown = castTime;
            // go to idle / play some "summoning new spell" animation
        }
        else if (distanceToPlayer > castRange * 1.1)
        {
            owner.Transition<Chase_BasicEnemy>();
        }
    }

    public override void Leave()
    {
        base.Leave();
        owner.Agent.isStopped = false;
    }


    private void CastFire()
    {
        //alter spawn location to avoid colliding with its own collider
        fireballSpawnLocation = owner.transform.position + Vector3.up.normalized * 1.5f + owner.gameObject.transform.forward * 2f;
        Vector3 oldAim = owner.gameObject.transform.forward * fireballSpeed;
        //cast spell
        Debug.Log("Firespell cast");
        GameObject fireball = Instantiate(spellPrefab, fireballSpawnLocation, Quaternion.identity);
        fireball.GetComponent<Rigidbody>().AddForce(oldAim, ForceMode.VelocityChange);
    }


    // could to separeate states for each but there would be a lot of kodupprepning
    private void CastWater()
    {
        Debug.Log("Waterspell cast");
        Vector3 eruptionSpot = owner.Player.transform.position + Vector3.up * -3f;
        Instantiate(spellPrefab, eruptionSpot, Quaternion.identity);
        EventManager.Current.FireEvent(new GeyserCastEvent(eruptionSpot, 6f)); //6f is range of extinguish. put this in geysher prefab script later.
    }

    private void CastEarth()
    {
        Debug.Log("earth cast");
        Quaternion spikesRotation = Quaternion.Euler(-90, owner.transform.rotation.eulerAngles.y, 0);
        GameObject earthSpikes = Instantiate(spellPrefab, owner.transform.position + owner.transform.forward * 2f, spikesRotation);
        earthSpikes.GetComponent<ParticleSystem>().Play();
    }

    private void CastWind()
    {
        Debug.Log("wind cast");
        GameObject windBlade = Instantiate(spellPrefab, owner.transform.position + Vector3.up.normalized + owner.transform.forward * 2f, owner.transform.rotation);
        windBlade.GetComponent<Rigidbody>().AddForce(Player.Instance.transform.forward * windBladeSpeed, ForceMode.VelocityChange);
    }

}
