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

    //FIREBALL variables
    private float fireballSpeed = 13f;
    private Vector3 fireballSpawnLocation;

    
    

    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);

        //Prepare variables to cast the right spell
        spells.Add("Fire", CastFire);
        spells.Add("Water", CastWater);
        elementType = owner.GetComponent<EnemySpellManager>().ElementType;
        spellPrefab = owner.GetComponent<EnemySpellManager>().SpellPrefab;
        castTime = owner.GetComponent<EnemySpellManager>().CastTime;
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

    public override void Update()
    {
        base.Update();
        //rotate / aim at player
        owner.transform.LookAt(owner.Player.transform.position);
        
        //tick down spell channel
        countdown -= Time.deltaTime;

        //animate enemy, sound
        
        //spellcast:
        if(countdown <= 0)
        {
            Debug.Log("Element type: " + elementType);
            spells[elementType]();
            countdown = castTime;
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
    }

}
