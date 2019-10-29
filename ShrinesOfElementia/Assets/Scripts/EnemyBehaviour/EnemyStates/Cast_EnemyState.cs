using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "InstantiateEnemyStates/Cast_EnemyState")]
public class Cast_EnemyState : BasicEnemyBaseState
{

    [SerializeField]private GameObject spellPrefab;
    private float castTime = 3.0f;
    private float countdown;

    //FIREBALL
    private float fireballSpeed = 13f;
    private Vector3 fireballSpawnLocation;

    //OTHER SPELLS?


    

    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
    }

    public override void Enter()
    {
        base.Enter();
        //stop and rotate / aim to player
        owner.Agent.destination = owner.Player.transform.position;
        owner.Agent.isStopped = true; // stop while casting

        //set timer
        countdown = 0.5f;


        

        
        //fetch spell to be cast from statemachine or other component on enemy.
    }

    public override void Update()
    {
        base.Update();

        //rotate / aim at player
        owner.transform.LookAt(owner.Player.transform.position);
        

        //tick down spell channel
        countdown -= Time.deltaTime;
        Debug.Log("countdown: " + countdown);

        //animate enemy, sound
        
        //spellcast:
        if(countdown <= 0)
        {
            castFire();
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


    private void castFire()
    {
        //prepare variables
        //alter spawn location to avoid colliding with its own collider
        fireballSpawnLocation = owner.transform.position;
        fireballSpawnLocation += owner.gameObject.transform.forward * 2f;
        fireballSpawnLocation += Vector3.up.normalized * 1.5f;
        Vector3 oldAim = owner.gameObject.transform.forward * fireballSpeed;
        //cast spell
        Debug.Log("Firespell cast");
        GameObject fireball = Instantiate(spellPrefab, fireballSpawnLocation, Quaternion.identity);
        fireball.GetComponent<Rigidbody>().AddForce(oldAim, ForceMode.VelocityChange);
    }


    // could to separeate states for each but there would be a lot of kodupprepning
    private void castWater()
    {
        Debug.Log("Waterspell cast");
    }

}
