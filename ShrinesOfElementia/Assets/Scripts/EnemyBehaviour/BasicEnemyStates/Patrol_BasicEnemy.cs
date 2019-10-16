using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InstantiateEnemyStates/BasicEnemyPatrol")]
public class Patrol_BasicEnemy : BasicEnemyBaseState
{
    private GameObject[] patrolPoints; //kinda unneccesary, might remove later idk
    private int currentTargetIndex;
    private float patrolSpeedModifier = 1.0f;
    private float distanceToPatrolPoint;
    

    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
    }

    public override void Start()
    {
        base.Start(); //for later
        patrolPoints = owner.PatrolPoints;
        currentTargetIndex = 0;
    }
    public override void Enter()
    {
        base.Enter();
        owner.Agent.speed *= patrolSpeedModifier;
        owner.Agent.SetDestination(patrolPoints[currentTargetIndex].transform.position);

    }

    public override void Update()
    {
        base.Update();

        distanceToPatrolPoint = Vector3.Distance(owner.transform.position, patrolPoints[currentTargetIndex].transform.position);

        //state Transistion checks: 
        //if on destination, transition to idle, update current target index
        if (distanceToPlayer <= 20)
        {
            Debug.Log("nu kommer jag och tar dig!");
            owner.Transition<Chase_BasicEnemy>();
        }
        else if (distanceToPatrolPoint <= 5.0f)
        {
            owner.Transition<Idle_BasicEnemy>();
        }
    }

    public override void Leave()
    {
        base.Leave();
        //change up patrol pattern 
        int nextIndex = (currentTargetIndex + 1) % patrolPoints.Length;
        currentTargetIndex = nextIndex;
    }

}
