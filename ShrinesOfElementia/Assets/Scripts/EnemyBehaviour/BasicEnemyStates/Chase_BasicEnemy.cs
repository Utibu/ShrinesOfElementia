//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InstantiateEnemyStates/BasicEnemyChase")]
public class Chase_BasicEnemy : BasicEnemyBaseState
{

    private Vector3 startPosition;
    private float chaseSpeed = 3.0f;


    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
    }


    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering chase state.");
        //modify agent to chase settings
        owner.Agent.speed = chaseSpeed;
        startPosition = owner.transform.position;

    }



    public override void Update()
    {
        base.Update();
        
        //do the chasing
        owner.Agent.SetDestination(player.transform.position);


        //State transition checks:
        if (distanceToPlayer > sightRange)
        {
            owner.Transition<Idle_BasicEnemy>();
        }
        else if (distanceToPlayer <= attackRange)
        {
            owner.Transition<Attack_BasicEnemy>();
        }

    }

    public override void Leave()
    {
        base.Leave();
        Debug.Log("Leaving chase state");
        owner.Agent.SetDestination(startPosition);
    }
}
