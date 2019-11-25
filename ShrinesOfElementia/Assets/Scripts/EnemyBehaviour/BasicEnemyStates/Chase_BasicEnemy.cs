//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InstantiateEnemyStates/BasicEnemyChase")]
public class Chase_BasicEnemy : BasicEnemyBaseState
{

    private Vector3 startPosition;
    private float chaseSpeed;

    private float timer;

    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
        chaseSpeed = speed * 1.5f;
    }


    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Entering chase state.");
        //modify agent to chase settings
        owner.Agent.speed = speed * 1.5f;
        startPosition = owner.transform.position;
        //owner.transform.rot
        timer = 0f;
    }



    public override void HandleUpdate()
    {
        base.HandleUpdate();

        timer += Time.deltaTime;
        if(timer > 2.5f)
        {
            timer = 0f;
            owner.Agent.speed *= Random.Range(0.6f, 1.4f);
        }

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
            owner.Agent.speed = 0.2f;
            owner.Transition<Attack_BasicEnemy>();
        }
        else if(owner.Elite && owner.Disabled == false && distanceToPlayer <= castRange)
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
