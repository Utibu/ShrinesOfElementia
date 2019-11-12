﻿//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InstantiateEnemyStates/BasicEnemyChase")]
public class Chase_BasicEnemy : BasicEnemyBaseState
{

    private Vector3 startPosition;
    private float chaseSpeed = 3.5f;


    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
    }


    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Entering chase state.");
        //modify agent to chase settings
        owner.Agent.speed = chaseSpeed;
        startPosition = owner.transform.position;
        //owner.transform.rot
    }



    public override void HandleUpdate()
    {
        base.HandleUpdate();
        
        //do the chasing
        owner.Agent.SetDestination(Player.Instance.transform.position);


        //State transition checks:
        if (distanceToPlayer > sightRange* 1.4)
        {
            owner.Agent.SetDestination(startPosition); // walk back and idle
            owner.Transition<Idle_BasicEnemy>();
        }
        else if (distanceToPlayer <= attackRange)
        {
            owner.Transition<Attack_BasicEnemy>();
        }
        else if(owner.Elite && distanceToPlayer <= castRange)
        {
            owner.Transition<Cast_EnemyState>();
        }

    }

    public override void Leave()
    {
        base.Leave();
        Debug.Log("Leaving chase state");
    }
}
