//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InstantiateEnemyStates/BasicEnemyChase")]
public class Chase_BasicEnemy : BasicEnemyBaseState
{

    private Vector3 startPosition;
    private float chaseSpeed;
    private float earlyChaseSpeed; // a boosted speed for the first few secs
    private float chaseStamina = 7f;
    private float timeInChase;

    private float timer;

    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
        chaseSpeed = speed * 1.5f;
        earlyChaseSpeed = speed * 2f;
        
    }


    public override void Enter()
    {
        base.Enter();

        //Debug.Log("Entering chase state.");
        //modify agent to chase settings
        owner.Agent.speed = earlyChaseSpeed;
        startPosition = owner.transform.position;
        //owner.transform.rot
        timer = 0f;
        timeInChase = 0;
        owner.Animator.SetBool("isChasing", true);
        TimerManager.Instance.SetNewTimer(owner.gameObject, 4f, SlowDownChase);
        owner.Agent.acceleration *= 4f;
    }



    public override void HandleUpdate()
    {
        base.HandleUpdate();
        timeInChase += Time.deltaTime;
        timer += Time.deltaTime;
        if(timer > 3.5f)
        {
            timer = 0f;
            owner.Agent.speed *= Random.Range(0.7f, 1.4f);
        }
        

        //do the chasing
        owner.Agent.SetDestination(Player.Instance.transform.position);

        //State transition checks:
        if (distanceToPlayer > enemyValues.SightRange * 3f || timeInChase > chaseStamina)
        {
            owner.Agent.SetDestination(startPosition); // walk back and idle
            owner.Transition<Idle_BasicEnemy>();
        }
        else if (distanceToPlayer <= attackRange)
        {
            owner.Agent.speed = 0.2f;
            owner.Agent.SetDestination(owner.transform.position);
            owner.Transition<Attack_BasicEnemy>();
        }
        else if(owner.Elite && owner.Disabled == false && distanceToPlayer <= castRange)
        {
            owner.Transition<Cast_EnemyState>();
        }

    }

    private void SlowDownChase()
    {
        owner.Agent.speed = chaseSpeed;
    }

    public override void Leave()
    {
        base.Leave();
        owner.Animator.SetBool("isChasing", false);
        Debug.Log("Leaving chase state");
        owner.Agent.acceleration /= 4f;
        
    }
}
