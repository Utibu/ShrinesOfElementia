// Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InstantiateEnemyStates/BasicEnemyPatrol")]
public class Patrol_BasicEnemy : BasicEnemyBaseState
{
    
    private int currentTargetIndex;
    private float patrolSpeed = 2.0f;
    private float distanceToPatrolPoint;
    

    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
    }

    public override void Start()
    {
        base.Start(); //for later
        currentTargetIndex = 0;
    }
    public override void Enter()
    {
        base.Enter();
        owner.Agent.speed = patrolSpeed;
        owner.Agent.SetDestination(owner.PatrolPoints[currentTargetIndex].transform.position);
        Debug.Log("Entering patrol state");
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();
        distanceToPatrolPoint = Vector3.Distance(owner.transform.position, owner.PatrolPoints[currentTargetIndex].transform.position);

        //state Transistion checks: 
        if (distanceToPlayer < sightRange)
        {
            Debug.Log("nu kommer jag och tar dig!");
            owner.Transition<Chase_BasicEnemy>();
        }
        else if (distanceToPatrolPoint <= 3.0f)
        {
            //change patrol point for next time
            int nextIndex = (currentTargetIndex + 1) % owner.PatrolPoints.Length;
            currentTargetIndex = nextIndex;
            owner.Transition<Idle_BasicEnemy>();
        }
    }

    public override void Leave()
    {
        base.Leave();
        Debug.Log("Leaving patrol state");
    }

}
